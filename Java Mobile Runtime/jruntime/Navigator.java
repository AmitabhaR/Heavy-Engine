/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import java.util.*;
/**
 *
 * @author Riju
 */
public class Navigator 
{
      private boolean isRunning = false;
        private Vector nav_points = new Vector();
        private int current_frame = 0;
        private GameObject_Scene baseObject;
        private int navigation_speed = 0;
        private float slope = 0f;
        private float deltaX = 0f, deltaY = 0f;
        private int pos_x = 0 , pos_y = 0;
        private int update_counter = 0, total_updates = 0;
        
        public boolean isCameraTranslationAllowed( )
        {
            return baseObject.AllowCameraTranslation;
        }

        public boolean isCameraRotationAllowed( ) 
        {
            return baseObject.AllowCameraRotation;         
        }
        
        public Navigator(GameObject_Scene baseObject,int navigation_speed)
        {
            this.baseObject = baseObject;
            this.navigation_speed = navigation_speed;
        }

        public void addPoint(Vector2 point )
        {
            if (!isRunning) nav_points.addElement(point);
        }

        public void deletePoint(int count)
        {
            if (count > -1 && count < nav_points.size( ))
            {
                nav_points.removeElementAt(count);
            }
        }
        
        private double pow(double num,int pow)
        {
            boolean isNegative = false;
            double sum = 1;
            
            if (pow == 0) return 1;
            if (pow < 0) { isNegative = true; pow *= -1; }
           
            for(int cntr = 1;cntr <= pow;cntr++) sum *= num;
            if (isNegative) sum = 1 / sum;
            
            return sum;
        }
        
        private void makePath(Vector2 begin_point , Vector2 end_point,boolean isPosChange)
        {
            deltaY = (end_point.y - begin_point.y);
            deltaX = (end_point.x - begin_point.x);

            if (Math.abs(deltaX) >= Math.abs(deltaY)) slope = deltaY / deltaX;
            else slope = deltaX / deltaY;

            total_updates = (int)(((float)Math.sqrt((double)pow(Math.abs(deltaX), 2) + pow(Math.abs(deltaY), 2))) / navigation_speed);
            
            if (isPosChange)
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

                if (current_frame < nav_points.size() && current_frame + 1 < nav_points.size())
                {
                    makePath((Vector2) nav_points.elementAt(current_frame), (Vector2) nav_points.elementAt(current_frame + 1),true); 
                    baseObject.pos_x = pos_x;
                    baseObject.pos_y = pos_y;
                }
            }
        }

        public boolean isNavigating()
        {
            return isRunning;
        }

        public void stop()
        {
            isRunning = false;
        }
        
        public void cameraUpdatePoints(Vector2 pos)
        {
            for(int cntr = 0;cntr < nav_points.size();cntr++)
            {
                ((Vector2) nav_points.elementAt(cntr)).x -= pos.x;
                ((Vector2) nav_points.elementAt(cntr)).y -= pos.y;
            }
        }
        
        public void cameraRotatePoints(float rotate_angle)
        {
            for (int cntr = 0; cntr < nav_points.size( ); cntr++)
            {
                Vector2 nav_point = (Vector2 ) nav_points.elementAt(cntr);
                // Point rotation.
                double angle = (rotate_angle * Math.PI / 180);
                double cos = Math.cos(angle);
                double sin = Math.sin(angle);
                int dx = nav_point.x - (HApplication.getActiveScene().getWidth() >> 1);
                int dy = nav_point.y - (HApplication.getActiveScene().getHeight() >> 1);
                
                nav_point.x = (int) (cos * dx - sin * dy + (HApplication.getActiveScene().getWidth() >> 1));
                nav_point.y = (int) (sin * dx + cos * dy + (HApplication.getActiveScene().getHeight() >> 1));
            }

            if (current_frame < nav_points.size() && current_frame + 1 < nav_points.size()) makePath((Vector2) nav_points.elementAt(current_frame),(Vector2) nav_points.elementAt(current_frame + 1), false);
        }
        
        public void update()
        {
            if (current_frame < nav_points.size( ) && current_frame + 1 < nav_points.size())
                if (this.baseObject.obj_instance.img != null)
                    if (update_counter == total_updates)
                    {
                        current_frame++;

                        if (current_frame < nav_points.size( ) && current_frame + 1 < nav_points.size( )) makePath(((Vector2) nav_points.elementAt(current_frame)), ((Vector2) nav_points.elementAt(current_frame + 1)),true);
                        else stop();
                    }
                    else
                    {
                        Vector2 deltaPos = new Vector2( 0 , 0 );

                        if (Math.abs(deltaX) >= Math.abs(deltaY))
                        {
                            deltaPos.x = navigation_speed * ((deltaX < 0) ? -1 : (deltaX > 0) ? 1 : 0);
                            deltaPos.y = (int)(slope * (pos_x - ((Vector2) nav_points.elementAt(current_frame)).x) + ((Vector2) nav_points.elementAt(current_frame)).y) - baseObject.pos_y;

                            pos_x += navigation_speed * ((deltaX < 0) ? -1 : (deltaX > 0) ? 1 : 0);
                        }
                        else
                        {
                            deltaPos.y = navigation_speed * ((deltaY < 0) ? -1 : (deltaY > 0) ? 1 : 0);
                            deltaPos.x = (int)(slope * (pos_y - ((Vector2) nav_points.elementAt(current_frame)).y) + ((Vector2) nav_points.elementAt(current_frame)).x) - baseObject.pos_x;

                            pos_y += navigation_speed * ((deltaY < 0) ? -1 : (deltaY > 0) ? 1 : 0);
                        }

                        baseObject.Translate(deltaPos.x, deltaPos.y);
                        update_counter++;
                    }
                else ;
            else stop();
        }
}
