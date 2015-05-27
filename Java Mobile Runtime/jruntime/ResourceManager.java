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
public class ResourceManager 
{
    public static Image findImage(String file_name)
	{
            java.io.InputStream fl = (new Object( )).getClass().getResourceAsStream("/Data/" + encryptFileName(file_name) + ".X");
            
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
            InputStream fl = (new Object( )).getClass().getResourceAsStream("/Data/" + encryptFileName(file_name) + ".X");
            
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
            InputStream fl = (new Object( )).getClass().getResourceAsStream("/Data/" + encryptFileName(file_name) + ".X");
            
		if (fl != null)
		{
                    return "/Data/" + encryptFileName(file_name) + ".X";
		}
		else
		{
                    return "";
		}
	}
        
        static boolean isLetter(char c)
        {
            if ((c >= 65 && c <= 91) || (c >= 97 && c <= 123))
            {
                return true;
            }
            
            return false;
        }
        
       public static String decryptFileName(String base_string)
        {
            String out_string = "";
        int skip_count = 0;
        
        if (base_string == null || base_string == "")
            {
                return "";
            }
        
        for(int cnt = 0;cnt < base_string.length();cnt++)
        {
            if (skip_count > 0 && skip_count <= 3)
            {
                skip_count++;
                continue;
            }
            else
            {
                skip_count = 0;
            }
            
            char cur_ch = base_string.charAt(cnt);
            
            if (isLetter(cur_ch))
            {
                if (base_string.charAt(cnt - 1) == '0')
                {
                      out_string += (char) (cur_ch - 10);
                }
                else
                {
                      out_string += (char) (cur_ch + 10);
                }
                
                skip_count = 1;
            }
            else
            {
                if (cnt == 0) continue;
                
                if (base_string.charAt(cnt - 1) == '2')
                {
                    out_string += cur_ch; // Avoid Digits.
                    skip_count = 3;
                }
            }
        }
        
        return out_string;
        }
        
         public static String encryptFileName(String base_string)
    {
        String out_string = "";
        
        if (base_string == null || base_string == "")
            {
                return "";
            }
        
        base_string = base_string.toUpperCase( );
        
        for(int cnt = 0;cnt < base_string.length();cnt++)
        {
            char cur_ch = base_string.charAt(cnt);
            
            if (cur_ch < 65 || cur_ch > 91)
            {
                 out_string += (int) 2;
                 out_string += cur_ch; // Avoid Digits.
            }
            else if (cur_ch + 10 < 91)
            {
                out_string += (int) 0;
                out_string += (char) (cur_ch + 10);
                out_string += (int) (91 - (cur_ch + 10));
                
                if (91 - (cur_ch + 10) < 10)
                {
                    out_string += (int) 0;
                }
            }
            else if (cur_ch - 10 >= 65)
            {
                out_string += (int) 1;
                out_string += (char) (cur_ch - 10);
                out_string += (int) ((cur_ch - 10) - 65);
                
                if ((cur_ch - 10) - 65 < 10)
                {
                    out_string += (int) 0;
                }
            }
        }
        
      //  out_string = out_string.toUpperCase();
        
        return out_string;
    }
}
