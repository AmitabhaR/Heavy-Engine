/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import javax.microedition.lcdui.*;

/**
 *
 * @author Riju
 */
public class GameObject 
{
    public String name;
    public String text;
    public int tag;
    public Image img;
    public boolean _static;
    public boolean physics;
    public boolean rigidbody;
    public boolean collider;
    public Font font = Font.getDefaultFont();
    public int txt_R,txt_G,txt_B;
}
