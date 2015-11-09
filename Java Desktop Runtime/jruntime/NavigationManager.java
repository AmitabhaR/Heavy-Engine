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
public class NavigationManager
{
      static ArrayList<Navigator> navigator_list = new ArrayList<Navigator>();
        
        public static void registerNavigation(Navigator navigator)
        {
            navigator_list.add(navigator);
            if (!navigator.isNavigating()) navigator.start();
        }

        public static void updateNavigation()
        {
            int cnt = 0;
            boolean toRepeat = false;
            
            do
            {
                toRepeat = false;
                
            for (; cnt < navigator_list.size( );cnt++ )
                {
                    Navigator nav = navigator_list.get(cnt);

                    if (!nav.isNavigating())
                    {
                       // nav.stop();
                        navigator_list.remove(cnt);
                        toRepeat = true;
                        break;
                    }
                    else
                    {
                        nav.update();
                    }
                }
            
            } while (toRepeat);
        }
        
        public static void updateNavigatorTargets(Vector2 pos) // Called by camera class only.
        {
            for(Navigator nav : navigator_list) if (nav.isCameraTranslationAllowed()) nav.cameraUpdatePoints(pos);
        }
        
        public static void updateNavigatorTargets(float rotate_angle)
        {
            for(Navigator nav : navigator_list) if (nav.isCameraRotationAllowed()) nav.cameraRotatePoints(rotate_angle);
        }
}
