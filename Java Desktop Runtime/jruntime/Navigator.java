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
        private ArrayList<Vector2> nav_points = new ArrayList<Vector2>();
        private int current_frame = 0;
        private GameObject_Scene baseObject;
        private int navigation_speed = 0;
        private float slope = 0f;
        private float deltaX = 0f, deltaY = 0f;
        private int pos_x = 0 , pos_y = 0;
        private int update_counter = 0, total_updates = 0;
        
        public Navigator(GameObject_Scene baseObject,int navigation_speed)
        {
            this.baseObject = baseObject;
            this.navigation_speed = navigation_speed;
        }

        public void addPoint(Vector2 point )
        {
            if (!isRunning) nav_points.add(point);
        }

        public void deletePoint(int count)
        {
            if (count > -1 && count < nav_points.size( ))
            {
                nav_points.remove(count);
            }
        }
        
        private void makePath(Vector2 begin_point , Vector2 end_point)
        {
            deltaY = (end_point.y - begin_point.y);
            deltaX = (end_point.x - begin_point.x);

            if (Math.abs(deltaX) >= Math.abs(deltaY)) slope = deltaY / deltaX;
            else slope = deltaX / deltaY;

            total_updates = (int)(((float)Math.sqrt((double)Math.pow(Math.abs(deltaX), 2) + Math.pow(Math.abs(deltaY), 2))) / navigation_speed);
            update_counter = 0;

            pos_x = begin_point.x;
            pos_y = begin_point.y;
        }
        
        public void start()
        {
            if (!isRunning)
            {
                isRunning = true;
                current_frame = 0;

                if (current_frame < nav_points.size( ) && current_frame + 1 < nav_points.size())
                {
                    makePath(nav_points.get(current_frame), nav_points.get(current_frame + 1)); 
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
            for(int cntr = 0;cntr < nav_points.size( );cntr++)
            {
                nav_points.get(cntr).x -= pos.x;
                nav_points.get(cntr).y -= pos.y;
            }
        }
        
        public void update()
        {
             if (current_frame < nav_points.size( ) && current_frame + 1 < nav_points.size())
                if (this.baseObject.obj_instance.img != null)
                    if (update_counter == total_updates)
                    {
                        current_frame++;

                        if (current_frame < nav_points.size( ) && current_frame + 1 < nav_points.size( )) makePath(nav_points.get(current_frame), nav_points.get(current_frame + 1));
                        else stop();
                    }
                    else
                    {
                        if (Math.abs(deltaX) >= Math.abs(deltaY))
                        {
                            baseObject.pos_x = pos_x;
                            baseObject.pos_y = (int)(slope * (pos_x - nav_points.get(current_frame).x) + nav_points.get(current_frame).y);
                            pos_x += navigation_speed * ((deltaX < 0) ? -1 : (deltaX > 0) ? 1 : 0);
                        }
                        else
                        {
                            baseObject.pos_y = pos_y;
                            baseObject.pos_x = (int)(slope * (pos_y - nav_points.get(current_frame).y) + nav_points.get(current_frame).x);
                            pos_y += navigation_speed * ((deltaY < 0) ? -1 : (deltaY > 0) ? 1 : 0);
                        }

                        update_counter++;
                    }
                else ;
            else stop();
        }
}
