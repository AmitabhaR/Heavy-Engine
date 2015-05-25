using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Runtime
{
     public struct GameObject
	{
	    public string name;
		public string text;
		public int tag;
		public Image img;
		public bool _static;
		public bool physics;
		public bool rigidbody;
		public bool collider;
        public string font_name;
        public int font_size;
        public Color color;
	}
	
	public class GameObject_Scene
	{
		public int pos_x;
		public int pos_y;
		public GameObject obj_instance;
		public string instance_name;
        private List<HeavyScript> scripts;
        public int depth;
        public bool isDestroyed = false;

        public GameObject_Scene()
        {
            scripts = new List<HeavyScript>();
        }

		public void Translate(int x , int y)
		{
		   pos_x += x;
		   pos_y += y;
		}
		
		public void setText(string text)
		{
			obj_instance.text = text;
		}
		
		public void setStatic(bool value)
		{
		    obj_instance._static = value;
		}
		
		public void isRigid(bool value)
		{
			obj_instance.rigidbody = value;
		}
		
		public void isCollider(bool value)
		{
			obj_instance.collider = value;
		}
		
		public void setTag(int tag)
		{
			obj_instance.tag = tag;
		}

        public void setColor(Color col)
        {
            obj_instance.color = col;
        }

        public void setFont(Font font)
        {
            obj_instance.font_name = font.Name;
            obj_instance.font_size = (int) font.Size;
        }

		public void setImage(Image img)
		{
			obj_instance.img = img;
		}

        public void registerScript(HeavyScript script)
        {
            scripts.Add(script);
        }

        public bool scriptsEmpty()
        {
            return (scripts.Count > 0) ? false : true;
        }

        public void processScripts()
        {
            foreach (HeavyScript script in scripts)
            {
                script.process(this);
            }
        }

        public GameObject_Scene Copy()
        {
            return this.MemberwiseClone();
        }

        protected GameObject_Scene MemberwiseClone()
        {
            return (GameObject_Scene)base.MemberwiseClone();
        }
	}

    public abstract class HeavyScript
    {
        public abstract void process(GameObject_Scene gameObject);
    }

    public class Particle : GameObject_Scene
    {
        public int direction;
        public int speed;
    }

    public class ParticleSystem
    {
        List<Particle> particle_list;
        Scene sceneHandle;
        
        public ParticleSystem( Scene sceneHandle )
        {
            particle_list = new List<Particle>();
            this.sceneHandle = sceneHandle;
        }

        public void addParticle(Particle particle)
        {
            while (!sceneHandle.loadGameObject(particle))
            {
                particle.instance_name += (new Random()).Next(particle.pos_x + 10, particle.pos_x * 10) + (new Random()).Next(particle.pos_y + 10, particle.pos_y * 10);
            }

            particle_list.Add(particle);
        }

        public void addParticle(string instance_name, string object_name, int pos_x, int pos_y,int direction,int speed)
        {
            Particle particle = new Particle();

            particle.instance_name = "@XZX_PARTICLE_" + (new Random()).Next(pos_x + 10, pos_x * 10) + (new Random()).Next(pos_y + 10, pos_y * 10) + instance_name; 
            particle.obj_instance = ObjectManager.findGameObjectWithName(object_name);
            particle.pos_x = pos_x;
            particle.pos_y = pos_y;
            particle.direction = direction;
            particle.speed = speed;

            while (!sceneHandle.loadGameObject(particle))
            {
                particle.instance_name += (new Random()).Next(pos_x + 10, pos_x * 10) + (new Random()).Next(pos_y + 10, pos_y * 10); 
            }

            particle_list.Add( particle );  
        }

        public void updateSystem()
        {
            re:

            for (int cnt = 0; cnt < particle_list.Count; cnt++)
            {
                if (particle_list[cnt].pos_x < 0 || particle_list[cnt].pos_x > HApplication.getWindowHandle().Width || particle_list[cnt].pos_y < 0 || particle_list[cnt].pos_y > HApplication.getWindowHandle().Height)
                {
                    sceneHandle.destroyGameObject(particle_list[cnt].instance_name);
                    particle_list.RemoveAt(cnt);

                    goto re;
                }
                else
                {
                    if (particle_list[cnt].scriptsEmpty())
                    {
                        if (particle_list[cnt].direction == 1)
                        {
                            particle_list[cnt].pos_y -= particle_list[cnt].speed; // Up
                        }
                        else if (particle_list[cnt].direction == 2)
                        {
                            particle_list[cnt].pos_x -= particle_list[cnt].speed; // Left
                            particle_list[cnt].pos_y -= particle_list[cnt].speed; // Up
                        }
                        else if (particle_list[cnt].direction == 3)
                        {
                            particle_list[cnt].pos_x -= particle_list[cnt].speed; // Left
                        }
                        else if (particle_list[cnt].direction == 4)
                        {
                            particle_list[cnt].pos_x -= particle_list[cnt].speed; // Left
                            particle_list[cnt].pos_y += particle_list[cnt].speed; // Down 
                        }
                        else if (particle_list[cnt].direction == 5)
                        {
                            particle_list[cnt].pos_y += particle_list[cnt].speed; // Down
                        }
                        else if (particle_list[cnt].direction == 6)
                        {
                            particle_list[cnt].pos_x += particle_list[cnt].speed; // Right
                            particle_list[cnt].pos_y += particle_list[cnt].speed; // Down
                        }
                        else if (particle_list[cnt].direction == 7)
                        {
                            particle_list[cnt].pos_x += particle_list[cnt].speed; // Right
                        }
                        else if (particle_list[cnt].direction == 8)
                        {
                            particle_list[cnt].pos_x += particle_list[cnt].speed; // Right
                            particle_list[cnt].pos_y -= particle_list[cnt].speed; // Up.
                        }
                    }
                }
            }
        }
    }

    public class Scene
	{
	    System.Windows.Forms.Timer gameTimer;
	    List<GameObject_Scene> object_array = new List<GameObject_Scene>( );
        PictureBox canvas;        
		public int A = 0,R = 0,G = 0,B = 0;
		public int speed = 1;
		public int gravity = 0;
		public string name = "";
		public delegate void onCollision_Handler(GameObject_Scene collider1,GameObject_Scene collider2);
        public onCollision_Handler onCollision;
        DrawableGameObject[] sortedArray;

        public struct DrawableGameObject
        {
            public int depth;
            public int index;
        }

        public Scene()
        {
            sortedArray = new DrawableGameObject[0];
        }

		public void startScene(PictureBox canvas)
		{
		 this.canvas = canvas;
		 gameTimer = new System.Windows.Forms.Timer();
		 gameTimer.Tick += updateScene;
		 canvas.Paint += drawScene;
		 gameTimer.Enabled = true;
		 gameTimer.Interval = speed;
		 gameTimer.Start( );
         makeSorting();
		}

        public bool checkSorted(DrawableGameObject[] index_array)
        {
            for (int cnt = 0; cnt < index_array.Length; cnt++)
            {
                if (cnt + 1 < index_array.Length)
                {
                    if (index_array[cnt].depth < index_array[cnt + 1].depth)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void sortElements(DrawableGameObject[] index_array)
        {
            for (int cnt = 0; cnt < index_array.Length; cnt++)
            {
                for (int c = cnt + 1; c < index_array.Length; c++)
                {
                    if (index_array[cnt].depth < index_array[c].depth)
                    {
                        DrawableGameObject cp = index_array[cnt];

                        index_array[cnt] = index_array[c];

                        index_array[c] = cp;

                        break;
                    }
                }
            }
        }

        private void makeSorting()
        {
            sortedArray = new DrawableGameObject[object_array.Count];

            for (int cnt = 0; cnt < object_array.Count; cnt++)
            {
                sortedArray[cnt].depth = object_array[cnt].depth;
                sortedArray[cnt].index = cnt;
            }

            while (!checkSorted(sortedArray))
            {
                sortElements(sortedArray);
            }
        }

		private void updateScene(object sender,EventArgs e)
		{
                bool hasDeleted = false;

            re:
                for (int cnt = 0; cnt < object_array.Count; cnt++)
                {
                        if (object_array[cnt].isDestroyed)
                        {
                            object_array.RemoveAt(cnt);
                            hasDeleted = true;
                            goto re;
                        }
                }

                if (hasDeleted) makeSorting();

                for (int cnt = 0; cnt < object_array.Count; cnt++)
                {
                        if (!object_array[cnt].obj_instance._static)
                        {
                            if (object_array[cnt].obj_instance.physics)
                            {
                                if (object_array[cnt].obj_instance.rigidbody)
                                {
                                    object_array[cnt].pos_y += gravity;
                                }

                                for (int cnt0 = 0; cnt0 < object_array.Count; cnt0++)
                                {
                                    if (cnt0 == cnt)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (object_array[cnt].obj_instance.img != null && object_array[cnt0].obj_instance.img != null && object_array[cnt].obj_instance.physics && object_array[cnt0].obj_instance.physics && object_array[cnt].obj_instance.collider && object_array[cnt0].obj_instance.collider)
                                        {
                                            if (object_array[cnt].pos_x + object_array[cnt].obj_instance.img.Width > object_array[cnt0].pos_x && object_array[cnt].pos_x < object_array[cnt0].pos_x + object_array[cnt0].obj_instance.img.Width && object_array[cnt].pos_y + object_array[cnt].obj_instance.img.Height > object_array[cnt0].pos_y && object_array[cnt].pos_y < object_array[cnt0].pos_y + object_array[cnt0].obj_instance.img.Height)
                                            {
                                                if (onCollision != null)
                                                {
                                                    onCollision(object_array[cnt], object_array[cnt0]);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        if (!object_array[cnt].scriptsEmpty())
                        {
                            object_array[cnt].processScripts();
                        }
                    }

                NavigationManager.updateNavigation();
                AnimationManager.updateAnimation();

                canvas.Refresh();
		}
		
		private void drawScene(object sender,PaintEventArgs e)
		{
                e.Graphics.Clear(Color.FromArgb(A, R, G, B));

                for (int cntr = 0; cntr < sortedArray.Length; cntr++)
                {
                    int cnt = sortedArray[cntr].index;

                        if (object_array[cnt].obj_instance.img != null)
                        {
                            e.Graphics.DrawImage(object_array[cnt].obj_instance.img, new Point(object_array[cnt].pos_x, object_array[cnt].pos_y));
                        }
                        else if (object_array[cnt].obj_instance.text != "")
                        {
                            e.Graphics.DrawString(object_array[cnt].obj_instance.text, new Font(object_array[cnt].obj_instance.font_name, object_array[cnt].obj_instance.font_size), new SolidBrush(object_array[cnt].obj_instance.color), new Point(object_array[cnt].pos_x, object_array[cnt].pos_y));
                        }
                }
		}
		
		public void endScene( )
		{
		 gameTimer.Stop( );		
		}
		
		public bool loadGameObject(GameObject_Scene gameObject)
		{
            if (gameObject.instance_name == "")
            {
                return false;
            }

            foreach (GameObject_Scene gameObj in object_array)
            {
                if (gameObj.instance_name == gameObject.instance_name)
                {
                    return false;
                }
            }

            object_array.Add(gameObject);
            makeSorting();

            return true;
		}

        public void destroyGameObject(string instance_name)
        {
            for (int cnt = 0; cnt < object_array.Count; cnt++)
            {
                if (object_array[cnt].instance_name == instance_name)
                {
                    object_array[cnt].isDestroyed = true;
                    return;
                }
            }
        }

        public GameObject_Scene findGameObject(string name)
		{
			for(int cnt = 0;cnt < object_array.Count;cnt++)
			{
               if (object_array[cnt].instance_name == name)
			   {
					return object_array[cnt];
			   }			   
			}
			
			return null;
		}

        public GameObject_Scene[] findGameObject(int tag)
        {
            GameObject_Scene[] ret_array = new GameObject_Scene[0];

            for (int cnt = 0; cnt < object_array.Count; cnt++)
            {
                if (object_array[cnt].obj_instance.tag == tag)
                {
                    Array.Resize<GameObject_Scene>(ref ret_array , ret_array.Length + 1);
                    ret_array[ret_array.Length - 1] = object_array[cnt];
                }
            }
          
            return ret_array;
        }

        public Scene Copy()
        {
            return this.MemberwiseClone();
        }

        protected Scene MemberwiseClone( )
        {
            return (Scene) base.MemberwiseClone();
        }
	}
	
    public struct Vector2
    {
        public int x;
        public int y;
		
		public Vector2( int x , int y)
		{
			this.x = x;
			this.y = y;
		}
    }

    public class NavigationManager
    {
        static List<Navigator> navigator_list = new List<Navigator>();
        
        public static void registerNavigation(Navigator navigator)
        {
            navigator_list.Add(navigator);
            if (!navigator.isNavigating()) navigator.start();
        }

        public static void updateNavigation()
        {
            int cnt = 0;

        x:

            for (; cnt < navigator_list.Count;cnt++ )
                {
                    Navigator nav = navigator_list[cnt];

                    if (!nav.isNavigating())
                    {
                       // nav.stop();
                        navigator_list.RemoveAt(cnt);
                        goto x;
                    }
                    else
                    {
                        nav.update();
                    }
                }
        }
    }

    public class AnimationManager
    {
        static List<Animation> animation_list = new List<Animation>();

        public static void registerAnimation(Animation animation)
        {
            animation_list.Add(animation);
            if (!animation.isPlaying()) animation.start();
        }

        public static void updateAnimation()
        {
            int cnt = 0;

            x:

            for(;cnt < animation_list.Count;cnt++)
            {
                Animation anim = animation_list[cnt];

                if (!anim.isPlaying( ))
                {
                    animation_list.RemoveAt(cnt);
                    goto x;
                }
                else
                {
                    anim.update();
                }
            }

        }
    }

    public class Navigator
    {
        private bool isRunning = false;
        private List<Vector2> nav_points = new List<Vector2>();
        private int current_frame = 0;
        private GameObject_Scene baseObject;
        private int navigation_speed = 0;
        private float delta_error = 0;
        private bool isVertical = false;
        private float error = 0f;
        private float deltaX = 0f;
        private float deltaY = 0f;

        public Navigator(GameObject_Scene baseObject,int navigation_speed)
        {
            this.baseObject = baseObject;
            this.navigation_speed = navigation_speed;
        }

        public void addPoint(Vector2 point )
        {
            if (!isRunning) nav_points.Add(point);
        }

        public void deletePoint(int count)
        {
            if (count > -1 && count < nav_points.Count)
            {
                nav_points.RemoveAt(count);
            }
        }

        public void start()
        {
            if (!isRunning)
            {
                isRunning = true;
                current_frame = 0;

                if (current_frame < nav_points.Count)
                {
                    if (nav_points[current_frame].x - baseObject.pos_x == 0)
                    {
                        isVertical = true;
                    }
                    else
                    {
                        deltaX = nav_points[current_frame].x - baseObject.pos_x;
                        deltaY = nav_points[current_frame].y - baseObject.pos_y;
                        delta_error = Math.Abs( nav_points[current_frame].y - baseObject.pos_y / nav_points[current_frame].x - baseObject.pos_x);
                        isVertical = false;
                        error = 0f;
                    }
                }
            }
        }

        public bool isNavigating()
        {
            return isRunning;
        }

        public void stop()
        {
            isRunning = false;
        }

        public void update()
        {
                    if (current_frame < nav_points.Count)
                    {
                        if (this.baseObject.obj_instance.img != null)
                        {
                            if (baseObject.pos_x + (baseObject.obj_instance.img.Width / 2) > nav_points[current_frame].x && baseObject.pos_x < nav_points[current_frame].x + 10 && baseObject.pos_y + (baseObject.obj_instance.img.Height / 2) > nav_points[current_frame].y && baseObject.pos_y < nav_points[current_frame].y + 10)
                            {
                                current_frame++;

                                if (current_frame < nav_points.Count)
                                {
                                    if (nav_points[current_frame].x - baseObject.pos_x == 0 )
                                    {
                                        isVertical = true;
                                    }
                                    else
                                    {
                                        deltaX = nav_points[current_frame].x - baseObject.pos_x;
                                        deltaY = nav_points[current_frame].y - baseObject.pos_y;
                                        delta_error = Math.Abs(nav_points[current_frame].y - baseObject.pos_y / nav_points[current_frame].x - baseObject.pos_x);
                                        isVertical = false;
                                        error = 0f;
                                    }
                                }
                            }
                            else
                            {
                                if (!isVertical)
                                {
                                    if (error >= 0.5f)
                                    {
                                        baseObject.pos_y += (nav_points[current_frame].y < baseObject.pos_y) ? -navigation_speed : navigation_speed;
                                        error /= (deltaX + navigation_speed * deltaY * deltaX + deltaY + navigation_speed);
                                    }
                                    else
                                    {
                                        baseObject.pos_x += (nav_points[current_frame].x < baseObject.pos_x) ? -navigation_speed : navigation_speed;
                                        error += delta_error;
                                    }
                                }
                                else
                                {
                                    baseObject.pos_y += (nav_points[current_frame].y < baseObject.pos_y) ? -navigation_speed : navigation_speed;
                                }
                            }
                        }
                    }
                    else
                    {
                        stop();
                    }
        }
    }

	public class Animation
	{
		private List<Image> images;
		private float update_delay = 0.0f;
		private float update_counter  = 0.0f;
		private int current_frame = 0;
		private bool canRepeat = false;
        private bool isRunning = false;
        private GameObject_Scene baseGameObject;

		public Animation(GameObject_Scene baseObject, float update_delay , bool repeat)
		{
            this.baseGameObject = baseObject;
			this.update_delay = update_delay;
			update_counter = 0.0f;
			canRepeat = repeat;
			images = new List<Image>( );
		}

        public void start()
        {
            if (!isRunning)
            {
                isRunning = true;
                current_frame = 0;
            }
        }

		public void addFrame(Image img)
		{
			if (img != null)
			{
				images.Add( img ); 
			}
		}

        public bool isPlaying()
        {
            return isRunning;
        }

		public void addFrame(string name)
		{
			if (name != null)
			{
				if (ResourceManager.getResource(name) != "")
				{
					images.Add(Image.FromFile( ResourceManager.getResource( name ) ));
				}
			}
		}

		public void setFrame(int frame_id, Image img)
		{
			if (frame_id > -1 && frame_id < images.Count)
			{
				images[frame_id] = img;
			} 
		}

		public void deleteFrame(int frame_id)
		{
			if (frame_id > -1 && frame_id < images.Count)
			{
				images.RemoveAt( frame_id );
			} 
		}

		public Image getFrame()
		{
			return images[current_frame]; 
		}

        public void stop()
        {
            isRunning = false;
        }

		public void update( )
		{
			if (update_counter >= update_delay)
			{
				if (current_frame == images.Count)
				{
                    if (canRepeat)
                    {
                        current_frame = 0;
                        this.baseGameObject.setImage(images[current_frame]);
                        update_counter = 0f;
                    }
                    else
                    {
                        isRunning = false;
                    }
				}
				else
				{
                    this.baseGameObject.setImage(images[current_frame]);
					current_frame++;
                    update_counter = 0f;
				}
			}
			else
			{
				update_counter += 1f;
			}	 
		}
	}

    public class ObjectManager
	{
       static GameObject[] gameObject_array = new GameObject[0];
	   
	   public static void loadObject(string name,string text,string img_path,int tag,bool isStatic,bool isPhysics,bool isRigid,bool isCollider)
	   {
	      Array.Resize<GameObject>(ref gameObject_array,gameObject_array.Length + 1);
		  GameObject instance = new GameObject( );
		  
		  instance.name = name;
		  instance.text = text;
		  if (File.Exists(img_path))
		  {
			instance.img = Image.FromFile(img_path);
		  }
		  else
		  {
		    instance.img = null;
		  }
		  instance.tag = tag;
		  instance._static = isStatic;
		  instance.physics = isPhysics;
		  instance.rigidbody = isRigid;
		  instance.collider = isCollider;
          instance.font_name = "Verdana";
          instance.font_size = 12;
          instance.color = Color.Red;
		  gameObject_array[gameObject_array.Length - 1] = instance;
	   }
	   
	   public static GameObject[ ] findGameObjectWithTag(int tag)
	   {
          GameObject[] ret_array = new GameObject[0];
            
          for(int cnt = 0;cnt < gameObject_array.Length;cnt++)
          {
		      if (gameObject_array[cnt].tag == tag)
			  {
                  Array.Resize<GameObject>(ref ret_array, ret_array.Length + 1);
                  ret_array[ret_array.Length - 1] = gameObject_array[cnt];			  
			  }
		  }		  
		  
		  return ret_array;
	   }
	   
	   public static GameObject findGameObjectWithName(string name)
	   {
		GameObject null_obj = new GameObject( );
	   
		  for(int cnt = 0;cnt < gameObject_array.Length;cnt++)
          {
		      if (gameObject_array[cnt].name == name)
			  {
                  return gameObject_array[cnt];			  
			  }
		  }		  
		  
		  return null_obj;
	   }
	}
	
	public class SceneManager
	{
         static Scene[] scene_array = new Scene[0];
		 
		 public static void addScene(Scene scene )
		 {
		       Array.Resize<Scene>(ref scene_array,scene_array.Length + 1);
			   scene_array[scene_array.Length - 1] = scene;
		 }
		 
		 public static Scene getScene(string name)
		 {
		     for(int cntr = 0;cntr < scene_array.Length;cntr++)
			 {
				if (scene_array[cntr].name == name)
				{
					return scene_array[cntr].Copy( );
				}
			 }
			 
			 return null;
		 }
	}
	
	public class ResourceManager
	{
		public static Image findImage(string file_name)
		{
		
			if (File.Exists(Application.StartupPath + "\\Data\\" + encryptFileName(file_name) + ".X"))
			{
				return Image.FromFile(Application.StartupPath + "\\Data\\" + encryptFileName(file_name) + ".X");
			}
			else
			{
				return null;
			}
		}
		
		public static StreamReader getResourceAsStream(string file_name)
		{
			if (File.Exists(Application.StartupPath + "\\Data\\" + encryptFileName(file_name) + ".X"))
			{
				return new StreamReader(Application.StartupPath + "\\Data\\" + encryptFileName(file_name) + ".X");
			}
			else
			{
				return null;
			}
		}
		
		public static string getResource(string file_name)
		{
			if (File.Exists(Application.StartupPath + "\\Data\\" + encryptFileName(file_name) + ".X"))
			{
				return Application.StartupPath + "\\Data\\" + encryptFileName(file_name) + ".X";
			}
			else
			{
				return "";
			}
		}

        public static string decryptFileName(string base_string)
        {
            string out_string = "";
            int skip_count = 0;

            if (String.IsNullOrEmpty(base_string)) return "";

            for (int cnt = 0; cnt < base_string.Length; cnt++)
            {
                if (skip_count > 0 && skip_count <= 3)
                {
                    skip_count++;
                    continue;
                }
                else
                {
                    skip_count = 0;
                }

                char cur_ch = base_string[cnt];

                if (Char.IsLetter(cur_ch))
                {
                    if (base_string[cnt - 1] == '0')
                    {
                        out_string += (char)(cur_ch - 10);
                    }
                    else
                    {
                        out_string += (char)(cur_ch + 10);
                    }

                    skip_count = 1;
                }
                else
                {
                    if (cnt == 0) continue;

                    if (base_string[cnt - 1] == '2')
                    {
                        out_string += cur_ch; // Avoid Digits.
                        skip_count = 3;
                    }
                }
            }

            return out_string;
        }

        public static String encryptFileName(String base_string)
        {
            string out_string = "";

            if (String.IsNullOrEmpty(base_string)) return "";

            base_string = base_string.ToUpper();

            for (int cnt = 0; cnt < base_string.Length; cnt++)
            {
                char cur_ch = base_string[cnt];

                if (cur_ch < 65 || cur_ch > 91)
                {
                    out_string += (int)2;
                    out_string += cur_ch; // Avoid Digits.
                }
                else if (cur_ch + 10 <= 91)
                {
                    out_string += (int)0;
                    out_string += (char)(cur_ch + 10);
                    out_string += (int)(91 - (cur_ch + 10));

                    if (91 - (cur_ch + 10) < 10)
                    {
                        out_string += (int)0;
                    }
                }
                else if (cur_ch - 10 >= 65)
                {
                    out_string += (int)1;
                    out_string += (char)(cur_ch - 10);
                    out_string += (int)((cur_ch - 10) - 65);

                    if ((cur_ch - 10) - 65 < 10)
                    {
                        out_string += (int)0;
                    }
                }
            }

            //  out_string = out_string.toUpperCase();

            return out_string;
        }
	}

    public class HApplication
    {
        static Scene cur_scene;
        static Form mainWindow;
        static PictureBox canvas;
        public delegate void onKeyPress_Handler(Keys keyCode);
        public delegate void onMouseDown_Handler(MouseButtons button);
        public static onKeyPress_Handler onKeyPress;
        public static onMouseDown_Handler onMouseDown;
        static Point mousePosition;

        public static void loadScene(Scene newScene)
        {
            cur_scene = newScene;
        }

        public static Scene getActiveScene()
        {
            return cur_scene;
        }

        public static void setSize(int width, int height)
        {
            mainWindow.Width = width;
            mainWindow.Height = height;
        }

        public static int[] getSize()
        {
            int[] size = { mainWindow.Width, mainWindow.Height };
            return size;
        }

        public static Form getWindowHandle()
        {
            return mainWindow;
        }

        private static void OnKeyDOWN(object sender, KeyEventArgs e)
        {
            if (onKeyPress != null)
            {
                onKeyPress(e.KeyCode);
            }
        }

        public static void Initialize(string project_name)
        {
            // Initialize and load stuffs.
            mainWindow = new Form();
            canvas = new PictureBox();
            canvas.SetBounds(0, 0, 600, 640);
            mainWindow.KeyDown += OnKeyDOWN;
            mainWindow.MouseDown += new MouseEventHandler(mainWindow_MouseDown);
            mainWindow.MouseMove += new MouseEventHandler(mainWindow_MouseMove);
            mainWindow.Text = project_name;
            mainWindow.MaximizeBox = mainWindow.MinimizeBox = false;
            mainWindow.ShowIcon = false;
            mainWindow.ShowInTaskbar = false;
            mainWindow.SetBounds(50, 50, 600, 640);
            mainWindow.Controls.Add(canvas);
            mainWindow.Show();
        }

        static void mainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition.X = e.X;
            mousePosition.Y = e.Y;
        }

        static void mainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (onMouseDown != null)
            {
                onMouseDown(e.Button);
            }
        }

        public static Point getMousePosition()
        {
            return mousePosition;
        }

        public static PictureBox getCanvas()
        {
            return canvas;
        }
    }
}
