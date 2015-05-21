package jruntime;

import java.util.ArrayList;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Riju
 */
public class SceneLoader
{
         static ArrayList scene_array = new ArrayList( );
	 
	 public static void addScene(Scene scene )
		 {
                       scene_array.add(scene);
		 }
		 
		 public static Scene getScene(String name)
		 {
		     for(int cntr = 0;cntr < scene_array.size( );cntr++)
			 {
				if (((Scene) scene_array.get(cntr)).name == name)
				{
                                    try
                                    {
					return (Scene ) ((Scene) scene_array.get(cntr)).clone();
                                    }
                                    catch(CloneNotSupportedException ex)
                                    {
                                        return null;        
                                    }
				}
			 }
			 
			 return null;
		 }
	}