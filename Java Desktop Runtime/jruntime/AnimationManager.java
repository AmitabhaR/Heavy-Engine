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
public class AnimationManager 
{
     static Vector animation_list = new Vector();

        public static void registerAnimation(Animation animation)
        {
            animation_list.addElement(animation);
            if (!animation.isPlaying()) animation.start();
        }

        public static void updateAnimation()
        {
            int cnt = 0;
            boolean toRepeat = false;
            
            do
            {
                toRepeat = false;
           
            for(;cnt < animation_list.size( );cnt++)
            {
                Animation anim = (Animation ) animation_list.elementAt(cnt);

                if (!anim.isPlaying( ))
                {
                    animation_list.removeElementAt(cnt);
                    toRepeat = true;
                    break;
                }
                else
                {
                    anim.update();
                }
            }
            
            } while (toRepeat);

        }
}
