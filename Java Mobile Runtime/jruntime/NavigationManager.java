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
      static Vector navigator_list = new Vector();
        
        public static void registerNavigation(Navigator navigator)
        {
            navigator_list.addElement(navigator);
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
                    Navigator nav =(Navigator)  navigator_list.elementAt(cnt);

                    if (!nav.isNavigating())
                    {
                       // nav.stop();
                        navigator_list.removeElementAt(cnt);
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
            for(int cntr = 0;cntr < navigator_list.size();cntr++) ((Navigator) navigator_list.elementAt(cntr)).cameraUpdatePoints(pos);
        }
}
