using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Net;

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
        public bool AllowCameraRotation = true;
        public bool AllowCameraTranslation = false;
        private float rotation_angle;
        private float scale_rate;
        private Image source_img;
        private List<GameObject_Scene> child_list;
        private bool isChildReady = false;

        public GameObject_Scene()
        {
            scripts = new List<HeavyScript>();
            child_list = new List<GameObject_Scene>();
        }

        public void Initialize()
        {
            source_img = obj_instance.img;
        }

        private void LoadAllChilds( GameObject_Scene gameObject )
        {
            for (int cnt = 0; cnt < gameObject.child_list.Count;cnt++ )
            {
                gameObject.child_list[cnt] = HApplication.getActiveScene().findGameObject(gameObject.child_list[cnt].instance_name);
                LoadAllChilds(gameObject.child_list[cnt]);
            }

            gameObject.isChildReady = true;
        }

        public GameObject_Scene FindChildWithName( string child_name )
        {
            foreach(GameObject_Scene gameObject in child_list)
            {
                if (gameObject.instance_name == child_name) return gameObject;
            }

            return null;
        }

        public List<GameObject_Scene> FindChildWithTag( int tag )
        {
            List<GameObject_Scene> gameObject_list = new List<GameObject_Scene>();

            foreach (GameObject_Scene gameObject in child_list)
            {
                if (gameObject.obj_instance.tag == tag) gameObject_list.Add(gameObject);
            }

            return (gameObject_list.Count > 0) ? gameObject_list : null;
        }

        public void UpdateChildPosition( int rate_x , int rate_y , bool isParent = true)
        {
            if (!isParent)
            {
                pos_x += rate_x;
                pos_y += rate_y;
            }

            foreach(GameObject_Scene gameObj in child_list)
            {
                gameObj.UpdateChildPosition(rate_x, rate_y, false);
            }
        }

        public void UpdateChildRotation(float rate_angle, bool isParent = true)
        {
            if (!isParent)
            {
                ApplyRotation(-(rate_angle + rotation_angle));
                rotation_angle += rate_angle;
            }

            foreach (GameObject_Scene gameObj in child_list)
            {
                double rad_angle = Math.PI * rate_angle / 180;
                double def_x = gameObj.pos_x - (pos_x + obj_instance.img.Width / 2);
                double def_y = gameObj.pos_y - (pos_y + obj_instance.img.Height / 2);

                gameObj.pos_x +=(int) Math.Round( Math.Cos(rad_angle) * def_x - Math.Sin(rad_angle) * def_y );
                gameObj.pos_y +=(int) Math.Round( Math.Sin(rad_angle) * def_x + Math.Cos(rad_angle) * def_y );

                gameObj.UpdateChildRotation(rate_angle, false);
            }
        }

        public void UpdateChildScale( float rate_scale , bool isParent = true)
        {
            if (!isParent) scale_rate += rate_scale;

            foreach (GameObject_Scene gameObj in child_list)
            {
                gameObj.UpdateChildScale(rate_scale, false);
            }
        }

        public void AddChild(string child_name)
        {
            GameObject_Scene gameObj = new GameObject_Scene();

            gameObj.instance_name = child_name;

            child_list.Add(gameObj);
        }

        public float GetRotationAngle( )
        {
            return rotation_angle;
        }

        public void SetRotationAngle(float angle)
        {
            float prev_angle = rotation_angle;

            ApplyRotation(-angle);
            rotation_angle = angle;

            if (!isChildReady) LoadAllChilds(this);
            UpdateChildRotation((angle - prev_angle));
        }

        public float GetScale()
        {
            return scale_rate;
        }

        public void SetScale(float scale)
        {
            float prev_scale = scale_rate;
            if (scale > 0) scale_rate = scale;

            if (!isChildReady) LoadAllChilds(this); 
            UpdateChildScale(scale - prev_scale);
        }

        private void ApplyRotation(float angle)
        {
            if (obj_instance.img != null)
            {
                double rad_angle = angle * Math.PI / 180;
                Bitmap new_img = new Bitmap((int)(source_img.Width * Math.Abs(Math.Cos(rad_angle)) + source_img.Height * Math.Abs(Math.Sin(rad_angle))), (int)(source_img.Width * Math.Abs(Math.Sin(rad_angle)) + source_img.Height * Math.Abs(Math.Cos(rad_angle))));
                Graphics g = Graphics.FromImage(new_img);

                new_img.SetResolution(source_img.HorizontalResolution, source_img.VerticalResolution);

                g.Clear(Color.Transparent);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.TranslateTransform(((new_img.Width - source_img.Width) / 2), ((new_img.Height - source_img.Height) / 2));
                g.TranslateTransform((float)(source_img.Size.Width / 2), (float)(source_img.Size.Height / 2));
                g.RotateTransform(angle);
                g.TranslateTransform(-(float)(source_img.Size.Width / 2), -(float)(source_img.Size.Height / 2));
                g.DrawImage(source_img, 0, 0);

                obj_instance.img = new_img;
            }
        }

        public void Rotate(float angle)
        {
            ApplyRotation(-(angle + rotation_angle));
            rotation_angle += angle;

            if (!isChildReady) LoadAllChilds(this);
            UpdateChildRotation(angle);
        }

		public void Translate(int x , int y)
		{
		   pos_x += x;
		   pos_y += y;

           if (!isChildReady) LoadAllChilds(this);
           UpdateChildPosition(x, y);
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

    public class Camera
    {
        static Vector2 camera_pos;
        static float camera_rotation;

        public static void TranslateCamera(Vector2 pos)
        {
            camera_pos.x += pos.x;
            camera_pos.y += pos.y;
            foreach(GameObject_Scene gameObject in HApplication.getActiveScene().getAllGameObjects())
                if (gameObject.AllowCameraTranslation) gameObject.Translate(-pos.x, -pos.y);

            NavigationManager.updateNavigatorTargets(pos);
        }

        public static void RotateCamera(float rotate_angle)
        {
            camera_rotation += rotate_angle;
            foreach (GameObject_Scene gameObject in HApplication.getActiveScene().getAllGameObjects()) 
                if (gameObject.AllowCameraRotation) gameObject.Rotate(rotate_angle);

            NavigationManager.updateNavigatorTargets(rotate_angle);
        }

        public static Vector2 getCameraPosition() { return camera_pos;  }
        public static float getCameraRotation() { return camera_rotation; }
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

        private bool checkSorted(DrawableGameObject[] index_array)
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

        private void sortElements(DrawableGameObject[] index_array)
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
                                            if (object_array[cnt].pos_x + object_array[cnt].obj_instance.img.Width > object_array[cnt0].pos_x && object_array[cnt].pos_x < object_array[cnt0].pos_x + object_array[cnt0].obj_instance.img.Width && object_array[cnt].pos_y + object_array[cnt].obj_instance.img.Height > object_array[cnt0].pos_y && object_array[cnt].pos_y < object_array[cnt0].pos_y + object_array[cnt0].obj_instance.img.Height && object_array[cnt].depth == object_array[cnt0].depth)
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
                NetworkManager.updateNetwork();

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
                            e.Graphics.DrawImage(new Bitmap(object_array[cnt].obj_instance.img, new Size(object_array[cnt].obj_instance.img.Size.Width + (int) object_array[cnt].GetScale(),object_array[cnt].obj_instance.img.Size.Height + (int) object_array[cnt].GetScale())), new Point(object_array[cnt].pos_x, object_array[cnt].pos_y));
                        }
                        else if (object_array[cnt].obj_instance.text != "")
                        {
                            e.Graphics.DrawString(object_array[cnt].obj_instance.text, new Font(object_array[cnt].obj_instance.font_name, object_array[cnt].obj_instance.font_size + object_array[cnt].GetScale( )), new SolidBrush(object_array[cnt].obj_instance.color), new Point(object_array[cnt].pos_x, object_array[cnt].pos_y));
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

        public List<GameObject_Scene> getAllGameObjects() { return object_array;  }
        
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

            for (; cnt < navigator_list.Count; cnt++)
            {
                Navigator nav = navigator_list[cnt];

                if (!nav.isNavigating())
                {
                    navigator_list.RemoveAt(cnt);
                    cnt--; // Stay at current position.
                }
                else nav.update();
            }
        }

        public static void updateNavigatorTargets(Vector2 pos) // Called by camera class only.
        {
            foreach(Navigator nav in navigator_list) if (nav.isCameraTranslationAllowed) nav.cameraUpdatePoints(pos);
        }

        public static void updateNavigatorTargets(float rotation_angle) // Called by camera class only.
        {
            foreach (Navigator nav in navigator_list) if (nav.isCameraRotationAllowed) nav.cameraRotatePoints(rotation_angle);
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

    public class NetworkPlayer
    {
        public Socket socket;
        public IPAddress ip;
        public bool isReady;
        public byte[] data_buffer;
    }

    public enum NetworkConstants
    {
        MESSAGE_FLAG_SERVER_ONLY = 0xA,
        MESSAGE_FLAG_EVERYONE = 0xB,
        CONNECTION_SERVER = 0xC,
        CONNECTION_CLIENT = 0xD,
        HANDLER_MESSAGE = 0x7,
        HANDLER_UPDATE_GAMEOBJECT = 0x10,
        HANDLER_DESTROY_GAMEOBJECT = 0x12,
        HANDLER_CREATE_GAMEOBJECT = 0x51,
        HANDLER_DISCONNECT_PLAYER = 0x61,
        HANDLER_CONNECTED = 0x30
    }

    public class NetworkManager
    {
        const int BUFFER_MESSAGE = 0x7;
        const int BUFFER_UPDATE_GAMEOBJECT = 0x10;
        const int BUFFER_CREATE_GAMEOBJECT = 0x51;
        const int BUFFER_DISCONNECT_PLAYER = 0x61;
        
        static List<NetworkPlayer> client_list = new List<NetworkPlayer>( );
        static bool isNetworkReady = false;
        static bool isAcceptReady = true;
        static NetworkPlayer basePlayer;
        static int user_type = 0;

        public delegate void OnConnected(NetworkPlayer netPlayer);
        public delegate void OnCreateGameObject(GameObject_Scene gameObject);
        public delegate void OnDestroyGameObject(GameObject_Scene gameObject);
        public delegate void OnMessageReceived(char[] buffer, int buffer_size);
        public delegate void OnPlayerDisconnected(NetworkPlayer net_player);
        public delegate void OnUpdateGameObject(GameObject_Scene gameObject);


        public static OnConnected onConnected_handler_list;
        public static OnCreateGameObject onCreate_handler_list;
        public static OnDestroyGameObject onDestroy_handler_list;
        public static OnMessageReceived onMessage_handler_list;
        public static OnPlayerDisconnected onDisconnected_handler_list;
        public static OnUpdateGameObject onUpdate_handler_list;

  

        public static int getConnectionType() { return user_type; }
        public static NetworkPlayer getNetworkPlayer() { return basePlayer; }
        public static bool isConnected() { return isNetworkReady;  }

        public static List<NetworkPlayer> getConnectedPlayers()
        {
	        if (user_type == (int) NetworkConstants.CONNECTION_CLIENT) return client_list;

	        return null;
        }

        public static bool startServer(int port)
        {
            basePlayer = new NetworkPlayer();
            basePlayer.ip = Dns.GetHostEntry(Dns.GetHostName( )).AddressList[0];
            basePlayer.socket = new Socket(basePlayer.ip.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            
            try
            {
                basePlayer.socket.Bind(new IPEndPoint(basePlayer.ip,port));
                basePlayer.socket.Listen(100);
            }
            catch(SocketException ex)
            {
                return false;
            }
           
            user_type = (int) NetworkConstants.CONNECTION_SERVER;
            isNetworkReady = true;

	        return true;
        }

        public static bool startServer(int port,bool isTestingPurpose)
        {
            basePlayer = new NetworkPlayer();
            basePlayer.ip = IPAddress.Parse("127.0.0.1");
            basePlayer.socket = new Socket(basePlayer.ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            try
            {
                basePlayer.socket.Bind(new IPEndPoint(basePlayer.ip, port));
                basePlayer.socket.Listen(100);
            }
            catch (SocketException ex)
            {
                return false;
            }

            user_type = (int)NetworkConstants.CONNECTION_SERVER;
            isNetworkReady = true;

            return true;
        }

        public static bool connectServer(string ip, int port)
        {
            basePlayer = new NetworkPlayer();
            basePlayer.ip = IPAddress.Parse(ip);
            basePlayer.socket = new Socket(basePlayer.ip.AddressFamily,SocketType.Stream,ProtocolType.Tcp);

            try
            {
                basePlayer.socket.Connect( basePlayer.ip , port);
            }
            catch(SocketException e)
            {
                return false;
            }

            user_type = (int) NetworkConstants.CONNECTION_CLIENT;
            isNetworkReady = true;

            return true;
        }

        public static bool connectServer(int port , bool isTestingPurpose)
        {
            basePlayer = new NetworkPlayer();
            basePlayer.ip = IPAddress.Parse("127.0.0.1");
            basePlayer.socket = new Socket(basePlayer.ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                basePlayer.socket.Connect(basePlayer.ip, port);
            }
            catch (SocketException e)
            {
                return false;
            }

            user_type = (int)NetworkConstants.CONNECTION_CLIENT;
            isNetworkReady = true;

            return true;
        }

        static string extractString(byte[] buffer , int start_pos )
        {
            int cnt = start_pos;
            string ret_string = "";

            for(;buffer[cnt] != 0;cnt++)
            {
                ret_string += (char) buffer[cnt];
            }

            return ret_string;
        }

        static void acceptCallback(IAsyncResult res)
        {
            try
            {
                NetworkPlayer newPlayer = new NetworkPlayer();

                newPlayer.socket = ((Socket)res.AsyncState).EndAccept(res);
                newPlayer.ip = ((IPEndPoint)newPlayer.socket.RemoteEndPoint).Address;
                newPlayer.isReady = true;

                client_list.Add(newPlayer);

                if (onConnected_handler_list != null) onConnected_handler_list(newPlayer);
                isAcceptReady = true;
            }
            catch(Exception e)
            {
            }
        }

       static void receiveCallback(IAsyncResult res)
        {
            try
            {
                NetworkPlayer netplayer = (NetworkPlayer)res.AsyncState;
                int bytesRead = netplayer.socket.EndReceive(res);

                if (bytesRead > 0)
                {
                    if (user_type == (int)NetworkConstants.CONNECTION_SERVER)
                    {
                        if (netplayer.data_buffer[0] == BUFFER_MESSAGE)
                        {
                            int buffer_size = BitConverter.ToInt32(netplayer.data_buffer, 1);
                            byte flag = netplayer.data_buffer[5];

                            if (flag == (byte)NetworkConstants.MESSAGE_FLAG_EVERYONE)
                            {
                                for (int cnt = 0; cnt < client_list.Count; cnt++)
                                {
                                    if (client_list[cnt] != netplayer)
                                    {
                                        client_list[cnt].socket.BeginSend(netplayer.data_buffer, 0, 512, 0, new AsyncCallback(sendCallback), netplayer);
                                    }
                                }
                            }

                            if (onMessage_handler_list != null) onMessage_handler_list(extractString(netplayer.data_buffer, 6).ToCharArray(), buffer_size);
                        }
                        else if (netplayer.data_buffer[0] == BUFFER_CREATE_GAMEOBJECT)
                        {
                            string object_name = extractString(netplayer.data_buffer, 1);
                            string baseobj_name = extractString(netplayer.data_buffer, object_name.Length + 2);
                            int pos_x = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 3);
                            int pos_y = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 7);
                            int depth = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 11);
                            GameObject_Scene gameObject = new GameObject_Scene();

                            gameObject.instance_name = object_name;
                            gameObject.obj_instance = ObjectManager.findGameObjectWithName(baseobj_name);
                            gameObject.pos_x = pos_x;
                            gameObject.pos_y = pos_y;
                            gameObject.depth = depth;
                            gameObject.isDestroyed = false;

                            HApplication.getActiveScene().loadGameObject(gameObject);

                            for (int cnt = 0; cnt < client_list.Count; cnt++)
                            {
                                if (client_list[cnt] != netplayer)
                                {
                                    client_list[cnt].socket.BeginSend(netplayer.data_buffer, 0, 512, 0, new AsyncCallback(sendCallback), netplayer);
                                }
                            }

                            if (onCreate_handler_list != null) onCreate_handler_list(gameObject);
                        }
                        else if (netplayer.data_buffer[0] == BUFFER_UPDATE_GAMEOBJECT)
                        {
                            string object_name = extractString(netplayer.data_buffer, 1);
                            string baseobj_name = extractString(netplayer.data_buffer, object_name.Length + 2);
                            int pos_x = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 3);
                            int pos_y = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 7);
                            int depth = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 11);
                            bool isDestroyed = (bool)Convert.ToBoolean(netplayer.data_buffer[object_name.Length + baseobj_name.Length + 15]);

                            GameObject_Scene gameObject = new GameObject_Scene();

                            if ((gameObject = HApplication.getActiveScene().findGameObject(object_name)) != null)
                            {
                                if (isDestroyed == true)
                                {
                                    HApplication.getActiveScene().destroyGameObject(object_name);

                                    if (onDestroy_handler_list != null) onDestroy_handler_list(gameObject);
                                }
                                else
                                {
                                    gameObject.instance_name = object_name;
                                    gameObject.obj_instance = ObjectManager.findGameObjectWithName(baseobj_name);
                                    gameObject.pos_x = pos_x;
                                    gameObject.pos_y = pos_y;
                                    gameObject.depth = depth;
                                    gameObject.isDestroyed = false;

                                    if (onUpdate_handler_list != null) onUpdate_handler_list(gameObject);
                                }
                            }

                            for (int cnt = 0; cnt < client_list.Count; cnt++)
                            {
                                if (client_list[cnt] != netplayer)
                                {
                                    client_list[cnt].socket.BeginSend(netplayer.data_buffer, 0, 512, 0, new AsyncCallback(sendCallback), netplayer);
                                }
                            }
                        }
                        else if (netplayer.data_buffer[0] == BUFFER_DISCONNECT_PLAYER)
                        {
                            if (onDisconnected_handler_list != null) onDisconnected_handler_list(netplayer);

                            client_list.Remove(netplayer);
                        }
                    }
                    else if (user_type == (int)NetworkConstants.CONNECTION_CLIENT)
                    {
                        if (netplayer.data_buffer[0] == BUFFER_MESSAGE)
                        {
                            int buffer_size = BitConverter.ToInt32(netplayer.data_buffer, 1);

                            if (onMessage_handler_list != null) onMessage_handler_list(extractString(netplayer.data_buffer, 6).ToCharArray(), buffer_size);
                        }
                        else if (netplayer.data_buffer[0] == BUFFER_CREATE_GAMEOBJECT)
                        {
                            string object_name = extractString(netplayer.data_buffer, 1);
                            string baseobj_name = extractString(netplayer.data_buffer, object_name.Length + 2);
                            int pos_x = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 3);
                            int pos_y = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 7);
                            int depth = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 11);
                            GameObject_Scene gameObject = new GameObject_Scene();

                            gameObject.instance_name = object_name;
                            gameObject.obj_instance = ObjectManager.findGameObjectWithName(baseobj_name);
                            gameObject.pos_x = pos_x;
                            gameObject.pos_y = pos_y;
                            gameObject.depth = depth;
                            gameObject.isDestroyed = false;

                            HApplication.getActiveScene().loadGameObject(gameObject);

                            if (onCreate_handler_list != null) onCreate_handler_list(gameObject);
                        }
                        else if (netplayer.data_buffer[0] == BUFFER_UPDATE_GAMEOBJECT)
                        {
                            string object_name = extractString(netplayer.data_buffer, 1);
                            string baseobj_name = extractString(netplayer.data_buffer, object_name.Length + 2);
                            int pos_x = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 3);
                            int pos_y = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 7);
                            int depth = BitConverter.ToInt32(netplayer.data_buffer, object_name.Length + baseobj_name.Length + 11);
                            bool isDestroyed = (bool)Convert.ToBoolean(netplayer.data_buffer[object_name.Length + baseobj_name.Length + 15]);

                            GameObject_Scene gameObject = new GameObject_Scene();

                            if ((gameObject = HApplication.getActiveScene().findGameObject(object_name)) != null)
                            {
                                if (isDestroyed == true)
                                {
                                    HApplication.getActiveScene().destroyGameObject(object_name);

                                    if (onDestroy_handler_list != null) onDestroy_handler_list(gameObject);
                                }
                                else
                                {
                                    gameObject.instance_name = object_name;
                                    gameObject.obj_instance = ObjectManager.findGameObjectWithName(baseobj_name);
                                    gameObject.pos_x = pos_x;
                                    gameObject.pos_y = pos_y;
                                    gameObject.depth = depth;
                                    gameObject.isDestroyed = false;

                                    if (onUpdate_handler_list != null) onUpdate_handler_list(gameObject);
                                }
                            }
                        }
                        else if (netplayer.data_buffer[0] == BUFFER_DISCONNECT_PLAYER)
                        {
                            if (onDisconnected_handler_list != null) onDisconnected_handler_list(netplayer);

                            isNetworkReady = false;
                            netplayer.socket.Close();
                        }
                    }
                }

                netplayer.isReady = true;
            }
           catch(Exception e)
            {

            }
        }

        public static void updateNetwork()
        {
	        if (!isNetworkReady) return;

	        if (user_type == (int) NetworkConstants.CONNECTION_SERVER)
	        {
		        // Server Part.
		      
                if (isAcceptReady)
                {
                    isAcceptReady = false;
                    basePlayer.socket.BeginAccept(new AsyncCallback(acceptCallback),basePlayer.socket);
                }

		        for (int cnt = 0; cnt < client_list.Count; cnt++) // Check all the ports.
		        {
                    if (client_list[cnt].isReady)
                    {
                        client_list[cnt].isReady = false;
                        client_list[cnt].data_buffer = new byte[512];
                        client_list[cnt].socket.BeginReceive(client_list[cnt].data_buffer,0,512,0,new AsyncCallback(receiveCallback),client_list[cnt]);
                    }
                }
            }
            else
            {
                if (basePlayer.isReady)
                {
                    basePlayer.isReady = false;
                    basePlayer.data_buffer = new byte[512];
                    basePlayer.socket.BeginReceive(basePlayer.data_buffer,0,512,0,new AsyncCallback(receiveCallback),basePlayer);
                }
            }
        }

        public static void disconnect()
        {
	        if (isNetworkReady)
	        {
		        if (user_type == (int) NetworkConstants.CONNECTION_SERVER)
		        {
			        for (int cnt = 0 ; cnt < client_list.Count; cnt++)
			        {
                        client_list[cnt].data_buffer = new byte[512];
                        client_list[cnt].data_buffer[0] = (byte) BUFFER_DISCONNECT_PLAYER;

                        client_list[cnt].socket.BeginSend(client_list[cnt].data_buffer,0,512,0,new AsyncCallback(sendCallback),client_list[cnt]);
			        }
		        }
		        else if (user_type == (int) NetworkConstants.CONNECTION_CLIENT)
		        {
			         basePlayer.data_buffer = new byte[512];
                     basePlayer.data_buffer[0] = (byte) BUFFER_DISCONNECT_PLAYER;

                     basePlayer.socket.BeginSend(basePlayer.data_buffer,0,512,0,new AsyncCallback(sendCallback),basePlayer);
		        }

		        isNetworkReady = false;
		        basePlayer.socket.Close(100);
	        }
        }

        static void LoadString(byte[] buffer , int startIndex , string src)
        {
            int cnt = 0;

	        for (; cnt < src.Length; cnt++)
	        {
		        buffer[startIndex + cnt] = (byte) src[cnt];
	        }

            buffer[startIndex + cnt] = 0;
        } 

        static void LoadInteger(byte[] buffer , int startIndex , int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

	        for (int cnt = 0; cnt < 4; cnt++) 
            { 
                buffer[startIndex + cnt] = bytes[cnt]; 
            }
        }

        static void sendCallback(IAsyncResult res)
        {
            try
            {
                ((NetworkPlayer)res.AsyncState).socket.EndSend(res);
            }
            catch(Exception e)
            {

            }
        }

        public static void createGameObject(GameObject_Scene gameObject)
        {
	        if (!isNetworkReady) return;

	        byte[] data_buffer = new byte[512];

	        data_buffer[0] = BUFFER_CREATE_GAMEOBJECT;
	        LoadString(data_buffer,1,gameObject.instance_name);
	        LoadString(data_buffer,2 + gameObject.instance_name.Length, gameObject.obj_instance.name);
	        LoadInteger(data_buffer,3 + gameObject.instance_name.Length + gameObject.obj_instance.name.Length,gameObject.pos_x);
	        LoadInteger(data_buffer,7 + gameObject.instance_name.Length + gameObject.obj_instance.name.Length,gameObject.pos_y);
	        LoadInteger(data_buffer,11 + gameObject.instance_name.Length + gameObject.obj_instance.name.Length ,gameObject.depth);

	        if (user_type == (int) NetworkConstants.CONNECTION_CLIENT)
	        {
                basePlayer.socket.BeginSend(data_buffer,0,512,0,new AsyncCallback(sendCallback),basePlayer);
	        }
	        else if (user_type == (int) NetworkConstants.CONNECTION_SERVER)
	        {
		        for (int cnt = 0; cnt < client_list.Count; cnt++)
		        {
			        client_list[cnt].socket.BeginSend(data_buffer,0,512,0,new AsyncCallback(sendCallback),client_list[cnt]);
		        }
	        }
        }

        public static void updateGameObject(GameObject_Scene gameObject)
        {
	        if (!isNetworkReady) return;

	         byte[] data_buffer = new byte[512];

	        data_buffer[0] = BUFFER_UPDATE_GAMEOBJECT;
	        LoadString(data_buffer,1,gameObject.instance_name);
	        LoadString(data_buffer,2 + gameObject.instance_name.Length, gameObject.obj_instance.name);
	        LoadInteger(data_buffer,3 + gameObject.instance_name.Length + gameObject.obj_instance.name.Length,gameObject.pos_x);
	        LoadInteger(data_buffer,7 + gameObject.instance_name.Length + gameObject.obj_instance.name.Length,gameObject.pos_y);
	        LoadInteger(data_buffer,11 + gameObject.instance_name.Length + gameObject.obj_instance.name.Length ,gameObject.depth);
	        data_buffer[15 + gameObject.instance_name.Length + gameObject.obj_instance.name.Length] = Convert.ToByte(gameObject.isDestroyed);

	        if (user_type == (int) NetworkConstants.CONNECTION_CLIENT)
	        {
		        basePlayer.socket.BeginSend(data_buffer,0,512,0,new AsyncCallback(sendCallback),basePlayer);
	        }
	        else if (user_type == (int) NetworkConstants.CONNECTION_SERVER)
	        {
		       for (int cnt = 0; cnt < client_list.Count; cnt++)
		        {
			        client_list[cnt].socket.BeginSend(data_buffer,0,512,0,new AsyncCallback(sendCallback),client_list[cnt]);
		        }
	        }
        }

        public static void sendMessage(string message, int flag)
        {
	        if (!isNetworkReady) return;

	        byte[] data_buffer = new byte[512];

	        data_buffer[0] = BUFFER_MESSAGE;
	        LoadInteger(data_buffer,1,message.Length);
	        data_buffer[message.Length + 5] = (byte) flag;
	        LoadString(data_buffer,message.Length + 6,message);

	        if (user_type == (int) NetworkConstants.CONNECTION_CLIENT)
	        {
                basePlayer.socket.BeginSend(data_buffer, 0, 512, 0, new AsyncCallback(sendCallback), basePlayer);
	        }
	        else if (user_type == (int) NetworkConstants.CONNECTION_SERVER)
	        {
                for (int cnt = 0; cnt < client_list.Count; cnt++)
                {
                    client_list[cnt].socket.BeginSend(data_buffer, 0, 512, 0, new AsyncCallback(sendCallback), client_list[cnt]);
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
        private float slope = 0f;
        private float deltaX = 0f, deltaY = 0f;
        private int pos_x = 0 , pos_y = 0;
        private int update_counter = 0, total_updates = 0;

        public bool isCameraTranslationAllowed
        {
            get { return baseObject.AllowCameraTranslation;}
        }

        public bool isCameraRotationAllowed
        {
            get { return baseObject.AllowCameraRotation; }
        }

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

        private void makePath(Vector2 begin_point , Vector2 end_point,bool AllowPosChange = true)
        {
            deltaY = (end_point.y - begin_point.y);
            deltaX = (end_point.x - begin_point.x);

            if (Math.Abs(deltaX) >= Math.Abs(deltaY)) slope = deltaY / deltaX;
            else slope = deltaX / deltaY;

            total_updates = (int)(((float)Math.Sqrt((double)Math.Pow(Math.Abs(deltaX), 2) + Math.Pow(Math.Abs(deltaY), 2))) / navigation_speed);

            if (AllowPosChange)
            {
                update_counter = 0;
                pos_x = begin_point.x;
                pos_y = begin_point.y;
            }
        }

        public void start()
        {
            if (!isRunning)
            {
                isRunning = true;
                current_frame = 0;

                if (current_frame < nav_points.Count && current_frame + 1 < nav_points.Count)
                {
                    makePath(nav_points[current_frame], nav_points[current_frame + 1]); 
                    baseObject.pos_x = pos_x;
                    baseObject.pos_y = pos_y;
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

        public void cameraUpdatePoints(Vector2 pos)
        {
            for(int cntr = 0;cntr < nav_points.Count;cntr++)
            {
                Vector2 nav_point = nav_points[cntr];

                nav_point.x -= pos.x;
                nav_point.y -= pos.y;

                nav_points[cntr] = nav_point;
            }
        }

        public void cameraRotatePoints(float rotate_angle)
        {
            for (int cntr = 0; cntr < nav_points.Count; cntr++)
            {
                Vector2 nav_point = nav_points[cntr];
                // Point rotation.
                double angle = -(rotate_angle * Math.PI / 180);
                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);
                int dx = nav_point.x - (HApplication.getWindowHandle().Width >> 1);
                int dy = nav_point.y - (HApplication.getWindowHandle().Height >> 1);
                
                nav_point.x = (int) (cos * dx - sin * dy + (HApplication.getWindowHandle().Width >> 1));
                nav_point.y = (int) (sin * dx + cos * dy + (HApplication.getWindowHandle().Height >> 1));
                
                nav_points[cntr] = nav_point;
            }

            if (current_frame < nav_points.Count && current_frame + 1 < nav_points.Count) makePath(nav_points[current_frame], nav_points[current_frame + 1], false);
        }

        public void update()
        {
            if (current_frame < nav_points.Count && current_frame + 1 < nav_points.Count)
                if (this.baseObject.obj_instance.img != null)
                    if (update_counter == total_updates)
                    {
                        current_frame++;

                        if (current_frame < nav_points.Count && current_frame + 1 < nav_points.Count) makePath(nav_points[current_frame], nav_points[current_frame + 1]);
                        else stop();
                    }
                    else
                    {
                        Vector2 deltaPos;

                        if (Math.Abs(deltaX) >= Math.Abs(deltaY))
                        {
                            deltaPos.x = navigation_speed * ((deltaX < 0) ? -1 : (deltaX > 0) ? 1 : 0);
                            deltaPos.y = (int)(slope * (pos_x - nav_points[current_frame].x) + nav_points[current_frame].y) - baseObject.pos_y;

                            /*  baseObject.pos_x = pos_x;
                                baseObject.pos_y = (int)(slope * (pos_x - nav_points[current_frame].x) + nav_points[current_frame].y);
                             */

                            pos_x += navigation_speed * ((deltaX < 0) ? -1 : (deltaX > 0) ? 1 : 0);
                        }
                        else
                        {
                            deltaPos.y = navigation_speed * ((deltaY < 0) ? -1 : (deltaY > 0) ? 1 : 0);
                            deltaPos.x = (int)(slope * (pos_y - nav_points[current_frame].y) + nav_points[current_frame].x) - baseObject.pos_x;

                            /* baseObject.pos_y = pos_y;
                               baseObject.pos_x = (int)(slope * (pos_y - nav_points[current_frame].y) + nav_points[current_frame].x); 
                             */

                            pos_y += navigation_speed * ((deltaY < 0) ? -1 : (deltaY > 0) ? 1 : 0);
                        }

                        baseObject.Translate(deltaPos.x, deltaPos.y);
                        update_counter++;
                    }
                else ;
            else stop();
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
                        this.baseGameObject.SetRotationAngle(this.baseGameObject.GetRotationAngle());
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
                    this.baseGameObject.SetRotationAngle(this.baseGameObject.GetRotationAngle());
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
                else if (cur_ch + 10 < 91)
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
