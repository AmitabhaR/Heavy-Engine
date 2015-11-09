/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

/**
 *
 * @author Riju
 */
public class Camera 
{
     static Vector2 camera_pos;
     static float camera_rotation;

     public static void TranslateCamera(Vector2 pos)
     {
            camera_pos.x += pos.x;
            camera_pos.y += pos.y;
            for(GameObject_Scene gameObject : HApplication.getActiveScene().getAllGameObjects())
                if (gameObject.AllowCameraTranslation) gameObject.Translate(-pos.x, -pos.y);
            
            NavigationManager.updateNavigatorTargets(pos);
     }

     public static void RotateCamera(float rotate_angle)
     {
            camera_rotation += rotate_angle;
            for(GameObject_Scene gameObject : HApplication.getActiveScene().getAllGameObjects()) 
                if (gameObject.AllowCameraRotation) gameObject.Rotate(-rotate_angle);
            
            NavigationManager.updateNavigatorTargets(-rotate_angle);
     }

     public static Vector2 getCameraPosition() { return camera_pos;  }
     public static double getCameraRotation() { return camera_rotation; }
}
