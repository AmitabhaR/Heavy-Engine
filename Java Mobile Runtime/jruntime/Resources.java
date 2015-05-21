/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import java.io.IOException;
import java.io.InputStream;
import javax.microedition.lcdui.Image;

/**
 *
 * @author Riju
 */
public class Resources 
{
    public static Image findImage(String file_name)
	{
            java.io.InputStream fl = (new Object( )).getClass().getResourceAsStream("/Data/" + file_name + ".X");
            
            if (fl != null)
            {
                try
                {
                    return Image.createImage(fl);
                }
                catch(IOException ax)
                {
                    return null;
                }
            }
            else
            {
		return null;
            }
	}
		
	public static InputStream getResourceAsStream(String file_name)
	{
            InputStream fl = (new Object( )).getClass().getResourceAsStream("/Data/" + file_name + ".X");
            
	    if (fl != null)
	    {
                return fl;
	    }
	    else
	    {
                return null;
            }
	}
		
	public static String getResource(String file_name)
	{
            InputStream fl = (new Object( )).getClass().getResourceAsStream("/Data/" + file_name + ".X");
            
		if (fl != null)
		{
                    return "/Data/" + file_name + ".X";
		}
		else
		{
                    return "";
		}
	}
}
