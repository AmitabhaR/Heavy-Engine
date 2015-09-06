/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import java.util.*;
import javax.microedition.midlet.MIDlet;

/**
 *
 * @author Riju
 */
public class HApplication 
{
    static Scene cur_scene;
    static String prj_name;
    static MIDlet mlet;
    
     public static void loadScene(Scene newScene)
     {
            cur_scene = newScene;
     }

     public static Scene getActiveScene()
     {
            return cur_scene;
     }
  
     public static void Initialize(String project_name,MIDlet midlet)
     {
            // Initialize and load stuffs.
            prj_name = project_name;
            mlet = midlet;
     }
     
     public static void exitApp( )
     { 
         mlet.notifyDestroyed();
     }
}
