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
     static ArrayList<Animation> animation_list = new ArrayList<Animation>();

        public static void registerAnimation(Animation animation)
        {
            animation_list.add(animation);
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
                Animation anim = animation_list.get(cnt);

                if (!anim.isPlaying( ))
                {
                    animation_list.remove(cnt);
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
