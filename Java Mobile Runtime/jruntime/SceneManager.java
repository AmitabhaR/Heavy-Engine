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
public class SceneManager 
{
     static Vector scene_array = new Vector( );
		 
	 public static void addScene(Scene scene )
		 {
                       scene_array.addElement(scene);
		 }
		 
		 public static Scene getScene(String name)
		 {
		     for(int cntr = 0;cntr < scene_array.size( );cntr++)
			 {
				if (((Scene) scene_array.elementAt(cntr)).name == name)
				{
					return (Scene) scene_array.elementAt(cntr);
				}
                                
			 }
		
			 return null;
		 }
                 
}
