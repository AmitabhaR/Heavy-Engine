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
        private float delta_error = 0;
        private boolean isVertical = false;
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
            if (!isRunning) nav_points.add(point);
        }

        public void deletePoint(int count)
        {
            if (count > -1 && count < nav_points.size( ))
            {
                nav_points.remove(count);
            }
        }

        public void start()
        {
            if (!isRunning)
            {
                isRunning = true;
                current_frame = 0;

                if (current_frame < nav_points.size( ))
                {
                    if (nav_points.get(current_frame).x - baseObject.pos_x == 0)
                    {
                        isVertical = true;
                    }
                    else
                    {
                        deltaX = nav_points.get(current_frame).x - baseObject.pos_x;
                        deltaY = nav_points.get(current_frame).y - baseObject.pos_y;
                        delta_error = Math.abs( deltaY / deltaX);
                        isVertical = false;
                        error = 0f;
                    }
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

        public void update()
        {
                    if (current_frame < nav_points.size( ))
                    {
                        if (this.baseObject.obj_instance.img != null)
                        {
                            if (baseObject.pos_x + (baseObject.obj_instance.img.getWidth( ) / 2) > nav_points.get(current_frame).x && baseObject.pos_x < nav_points.get(current_frame).x + 10 && baseObject.pos_y + (baseObject.obj_instance.img.getHeight( ) / 2) > nav_points.get(current_frame).y && baseObject.pos_y < nav_points.get(current_frame).y + 10)
                            {
                                current_frame++;

                                if (current_frame < nav_points.size( ))
                                {
                                    if (nav_points.get(current_frame).x - baseObject.pos_x == 0 )
                                    {
                                        isVertical = true;
                                    }
                                    else
                                    {
                                         deltaX = nav_points.get(current_frame).x - baseObject.pos_x;
                                         deltaY = nav_points.get(current_frame).y - baseObject.pos_y;
                                         delta_error = Math.abs( deltaY / deltaX);
                                         isVertical = false;
                                         error = 0f;
                                    }
                                }
                            }
                            else
                            {
                              /*  if (baseObject.pos_x < nav_points[current_frame].x)
                                {
                                    baseObject.pos_x += navigation_speed;
                                }
                                else if (baseObject.pos_x > nav_points[current_frame].x)
                                {
                                    baseObject.pos_x -= navigation_speed;
                                }

                                if (baseObject.pos_y < nav_points[current_frame].y)
                                {
                                    baseObject.pos_y += navigation_speed;
                                }
                                else if (baseObject.pos_y > nav_points[current_frame].y)
                                {
                                    baseObject.pos_y -= navigation_speed;
                                } */
                                if (!isVertical)
                                {
                                    if (error >= 0.5f)
                                    {
                                        baseObject.pos_y += (nav_points.get(current_frame).y < baseObject.pos_y) ? -navigation_speed : navigation_speed;
                                        error /= (deltaX + navigation_speed * deltaY * deltaX + deltaY + navigation_speed);
                                    }
                                    else
                                    {
                                        baseObject.pos_x += (nav_points.get(current_frame).x < baseObject.pos_x) ? -navigation_speed : navigation_speed;
                                        error += delta_error;
                                    }
                                }
                                else
                                {
                                    baseObject.pos_y += (nav_points.get(current_frame).y < baseObject.pos_y) ? -navigation_speed : navigation_speed;
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
