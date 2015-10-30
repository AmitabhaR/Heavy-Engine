/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package jruntime;

import java.net.*;
import java.util.*;
import java.io.*;
import java.nio.ByteBuffer;
/**
 *
 * @author Riju
 */
public class NetworkManager 
{
        static final int BUFFER_MESSAGE = 0x7;
        static final int BUFFER_UPDATE_GAMEOBJECT = 0x10;
        static final int BUFFER_CREATE_GAMEOBJECT = 0x51;
        static final int BUFFER_DISCONNECT_PLAYER = 0x61;
        
        static ArrayList<NetworkPlayer> client_list = new ArrayList<NetworkPlayer>( );
        static boolean isNetworkReady = false;
        static NetworkPlayer basePlayer;
        static int user_type = 0;
        
        public static ArrayList<OnConnected> onConnected_handler_list = new ArrayList<OnConnected>( );
        public static ArrayList<OnCreateGameObject> onCreate_handler_list = new ArrayList<OnCreateGameObject>( );
        public static ArrayList<OnDestroyGameObject> onDestroy_handler_list = new ArrayList<OnDestroyGameObject>( );
        public static ArrayList<OnMessageReceived> onMessage_handler_list = new ArrayList<OnMessageReceived>( );
        public static ArrayList<OnPlayerDisconnected> onDisconnected_handler_list = new ArrayList<OnPlayerDisconnected>( );
        public static ArrayList<OnUpdateGameObject> onUpdate_handler_list = new ArrayList<OnUpdateGameObject>( );

        public static int getConnectionType() { return user_type; }
        public static NetworkPlayer getNetworkPlayer() { return basePlayer; }
        public static boolean isConnected() { return isNetworkReady;  }

        public static ArrayList<NetworkPlayer> getConnectedPlayers()
        {
	        if (user_type == NetworkConstants.CONNECTION_CLIENT) return client_list;

	        return null;
        }

        public static boolean startServer(int port)
        {
            basePlayer = new NetworkPlayer();
            
            try
            {
                basePlayer.server_sock = new ServerSocket(port);
                basePlayer.ip = basePlayer.server_sock.getInetAddress();
                basePlayer.server_sock.setSoTimeout(100);
            }
            catch(IOException eax)
            {
                return false;
            }
        
            user_type = NetworkConstants.CONNECTION_SERVER;
            isNetworkReady = true;

	     return true;
        }

        public static boolean startServer(int port,boolean isTestingPurpose)
        {
            basePlayer = new NetworkPlayer();
           
            try
            {
                basePlayer.ip = InetAddress.getByName("localhost");
                basePlayer.server_sock = new ServerSocket(port,100,basePlayer.ip);
                basePlayer.server_sock.setSoTimeout(100);
            }
            catch (IOException ex)
            {
                return false;
            }

            user_type = NetworkConstants.CONNECTION_SERVER;
            isNetworkReady = true;

            return true;
        }

        public static boolean connectServer(String ip,int port)
        {
            basePlayer = new NetworkPlayer();
            
            try
            {
                basePlayer.socket = new Socket(ip,port);
                basePlayer.ip = basePlayer.socket.getInetAddress();
            }
            catch(IOException e)
            {
                return false;
            }

            user_type = (int) NetworkConstants.CONNECTION_CLIENT;
            isNetworkReady = true;

            return true;
        }

        public static boolean connectServer(int port , boolean isTestingPurpose)
        {
            basePlayer = new NetworkPlayer();
            
            try
            {
                basePlayer.ip = InetAddress.getByName("localhost");
                basePlayer.socket = new Socket("localhost",port);
            }
            catch (IOException e)
            {
                return false;
            }

            user_type = (int)NetworkConstants.CONNECTION_CLIENT;
            isNetworkReady = true;

            return true;
        }

        static String extractString(byte[] buffer , int start_pos )
        {
            int cnt = start_pos;
            String ret_string = "";

            for(;buffer[cnt] != 0;cnt++)
            {
                ret_string += (char) buffer[cnt];
            }

            return ret_string;
        }

      
        public static void updateNetwork()
        {
	        if (!isNetworkReady) return;
                
                byte[] data_buffer = new byte[512];
                
	        if (user_type == (int) NetworkConstants.CONNECTION_SERVER)
	        {
		    // Server Part.
                    Socket new_sock = null;
                    
                    try
                    {
                        Socket sock = basePlayer.server_sock.accept();
                        NetworkPlayer newPlayer = new NetworkPlayer();

                        newPlayer.socket = sock;
                        newPlayer.ip = newPlayer.socket.getInetAddress();
                
                        client_list.add(newPlayer);

                        for(int cnt = 0;cnt < onConnected_handler_list.size();cnt++)
                        {
                            onConnected_handler_list.get(cnt).processCall(newPlayer);
                        }
                    }
                    catch(IOException eax)
                    {
                        
                    }

		    for (int cnt = 0; cnt < client_list.size(); cnt++) // Check all the ports.
		    {
                        InputStream inp_stm = null;
                        
                       try { inp_stm = client_list.get(cnt).socket.getInputStream(); } catch (IOException ex)  { }
                       
                        
                        try
                        {
                        
                            if (inp_stm.available() > 0)
                            {
                                inp_stm.read(data_buffer);
                            }
                        }
                        catch(IOException ex)
                        {
                            
                        }
                        
                        if (data_buffer[0] == BUFFER_MESSAGE)
                        {
                            int buffer_size = ToInt32(data_buffer, 1);
                            byte flag = data_buffer[5];

                            if (flag == (byte)NetworkConstants.MESSAGE_FLAG_EVERYONE)
                            {
                                for (int c = 0; c < client_list.size(); c++)
                                {
                                    if (client_list.get(cnt) != client_list.get(c))
                                    {
                                        try
                                        {
                                            client_list.get(c).socket.getOutputStream().write(data_buffer, 0, 512);
                                            client_list.get(c).socket.getOutputStream().flush();
                                        }
                                        catch(IOException ex)
                                        {
                                            
                                        }
                                    }
                                }
                            }

                            for(int c = 0;cnt < onMessage_handler_list.size();c++)
                            {
                                onMessage_handler_list.get(c).processMessageReceived(extractString(data_buffer, 6).getBytes(), buffer_size);
                            }
                        }
                        else if (data_buffer[0] == BUFFER_CREATE_GAMEOBJECT)
                        {
                            String object_name = extractString(data_buffer, 1);
                            String baseobj_name = extractString(data_buffer, object_name.length() + 2);
                            int pos_x = ToInt32(data_buffer, object_name.length() + baseobj_name.length( ) + 3);
                            int pos_y = ToInt32(data_buffer, object_name.length( ) + baseobj_name.length( ) + 7);
                            int depth = ToInt32(data_buffer, object_name.length () + baseobj_name.length ( ) + 11);
                            GameObject_Scene gameObject = new GameObject_Scene();

                            gameObject.instance_name = object_name;
                            gameObject.obj_instance = ObjectManager.findGameObjectWithName(baseobj_name);
                            gameObject.pos_x = pos_x;
                            gameObject.pos_y = pos_y;
                            gameObject.depth = depth;
                            gameObject.isDestroyed = false;

                            HApplication.getActiveScene().loadGameObject(gameObject);

                            for (int c = 0; c < client_list.size( ); c++)
                            {
                                if (client_list.get(cnt) != client_list.get(c))
                                {
                                    try
                                    {
                                        client_list.get(c).socket.getOutputStream().write(data_buffer,0,512);
                                        client_list.get(c).socket.getOutputStream().flush();
                                    }
                                    catch(IOException ex)
                                    {
                                        
                                    }
                                }
                            }

                            for(int c = 0;c < onCreate_handler_list.size();c++)
                            {
                                onCreate_handler_list.get(c).processCreateGameObject(gameObject);
                            }
                        }
                        else if (data_buffer[0] == BUFFER_UPDATE_GAMEOBJECT)
                        {
                            String object_name = extractString(data_buffer, 1);
                            String baseobj_name = extractString(data_buffer, object_name.length() + 2);
                            int pos_x = ToInt32(data_buffer, object_name.length() + baseobj_name.length() + 3);
                            int pos_y = ToInt32(data_buffer, object_name.length() + baseobj_name.length() + 7);
                            int depth = ToInt32(data_buffer, object_name.length() + baseobj_name.length() + 11);
                            boolean isDestroyed = (data_buffer[object_name.length() + baseobj_name.length() + 15] == 1) ? true : false;

                            GameObject_Scene gameObject = new GameObject_Scene();

                            if ((gameObject = HApplication.getActiveScene().findGameObject(object_name)) != null)
                            {
                                if (isDestroyed == true)
                                {
                                    HApplication.getActiveScene().destroyGameObject(object_name);

                                    if (onDestroy_handler_list != null) 
                                    {
                                        for(int c = 0;c < onDestroy_handler_list.size();c++)
                                        {
                                            onDestroy_handler_list.get(c).processOnDestroyGameObject(gameObject);
                                        }
                                    }
                                }
                                else
                                {
                                    gameObject.instance_name = object_name;
                                    gameObject.obj_instance = ObjectManager.findGameObjectWithName(baseobj_name);
                                    gameObject.pos_x = pos_x;
                                    gameObject.pos_y = pos_y;
                                    gameObject.depth = depth;
                                    gameObject.isDestroyed = false;

                                    if (onUpdate_handler_list != null) 
                                    {
                                        for(int c = 0;c < onUpdate_handler_list.size();c++)
                                        {
                                            onUpdate_handler_list.get(c).processOnUpdateGameObject(gameObject);
                                        }
                                    }
                                }
                            }
                            
                            for (int c = 0; c < client_list.size(); c++)
                            {
                                if (client_list.get(c) != client_list.get(cnt))
                                {
                                    try
                                    {
                                        client_list.get(c).socket.getOutputStream().write(data_buffer, 0, 512);
                                        client_list.get(c).socket.getOutputStream().flush();
                                    }
                                    catch(IOException ex)
                                    {
                                        
                                    }
                                }
                            }
                        }
                        else if (data_buffer[0] == BUFFER_DISCONNECT_PLAYER)
                        {
                             if (onDisconnected_handler_list != null) 
                             {
                                 for(int c = 0;c < onDisconnected_handler_list.size();c++)
                                 {
                                    onDisconnected_handler_list.get(c).processOnPlayerDisconnected(client_list.get(cnt));
                                 }
                             }

                            isNetworkReady = false;
                            try { client_list.get(cnt).socket.close(); } catch (IOException ex) {  }
                        }
                    }
                }
                else
                {
                    InputStream inp_stm = null;
                        
                    try { inp_stm = basePlayer.socket.getInputStream(); } catch (IOException ex)  { }
                       
                        
                        try
                        {
                        
                            if (inp_stm.available() > 0)
                            {
                                inp_stm.read(data_buffer);
                            }
                        }
                        catch(IOException ex)
                        {
                            
                        }
                        
                        if (data_buffer[0] == BUFFER_MESSAGE)
                        {
                            int buffer_size = ToInt32(data_buffer, 1);

                            if (onMessage_handler_list != null)
                            {
                                for(int c = 0;c < onMessage_handler_list.size();c++)
                                {
                                    onMessage_handler_list.get(c).processMessageReceived(extractString(data_buffer, 6).getBytes(), buffer_size);
                                }
                            }
                        }
                        else if (data_buffer[0] == BUFFER_CREATE_GAMEOBJECT)
                        {
                            String object_name = extractString(data_buffer, 1);
                            String baseobj_name = extractString(data_buffer, object_name.length() + 2);
                            int pos_x = ToInt32(data_buffer, object_name.length() + baseobj_name.length( ) + 3);
                            int pos_y = ToInt32(data_buffer, object_name.length( ) + baseobj_name.length( ) + 7);
                            int depth = ToInt32(data_buffer, object_name.length () + baseobj_name.length ( ) + 11);
                            GameObject_Scene gameObject = new GameObject_Scene();

                            gameObject.instance_name = object_name;
                            gameObject.obj_instance = ObjectManager.findGameObjectWithName(baseobj_name);
                            gameObject.pos_x = pos_x;
                            gameObject.pos_y = pos_y;
                            gameObject.depth = depth;
                            gameObject.isDestroyed = false;

                            HApplication.getActiveScene().loadGameObject(gameObject);
                            
                            for(int c = 0;c < onCreate_handler_list.size();c++)
                            {
                                onCreate_handler_list.get(c).processCreateGameObject(gameObject);
                            }
                        }
                        else if (data_buffer[0] == BUFFER_UPDATE_GAMEOBJECT)
                        {
                            String object_name = extractString(data_buffer, 1);
                            String baseobj_name = extractString(data_buffer, object_name.length() + 2);
                            int pos_x = ToInt32(data_buffer, object_name.length() + baseobj_name.length() + 3);
                            int pos_y = ToInt32(data_buffer, object_name.length() + baseobj_name.length() + 7);
                            int depth = ToInt32(data_buffer, object_name.length() + baseobj_name.length() + 11);
                            boolean isDestroyed = (data_buffer[object_name.length() + baseobj_name.length() + 15] == 1) ? true : false;

                            GameObject_Scene gameObject = new GameObject_Scene();

                            if ((gameObject = HApplication.getActiveScene().findGameObject(object_name)) != null)
                            {
                                if (isDestroyed == true)
                                {
                                    HApplication.getActiveScene().destroyGameObject(object_name);

                                    if (onDestroy_handler_list != null) 
                                    {
                                        for(int c = 0;c < onDestroy_handler_list.size();c++)
                                        {
                                            onDestroy_handler_list.get(c).processOnDestroyGameObject(gameObject);
                                        }
                                    }
                                }
                                else
                                {
                                    gameObject.instance_name = object_name;
                                    gameObject.obj_instance = ObjectManager.findGameObjectWithName(baseobj_name);
                                    gameObject.pos_x = pos_x;
                                    gameObject.pos_y = pos_y;
                                    gameObject.depth = depth;
                                    gameObject.isDestroyed = false;

                                    if (onUpdate_handler_list != null) 
                                    {
                                        for(int c = 0;c < onUpdate_handler_list.size();c++)
                                        {
                                            onUpdate_handler_list.get(c).processOnUpdateGameObject(gameObject);
                                        }
                                    }
                                }
                            }
                        }
                }
        }

        public static void disconnect()
        {
	        if (isNetworkReady)
	        {
                      byte[] data_buffer = new byte[512];
                    
                      data_buffer = new byte[512];
                      data_buffer[0] = (byte) BUFFER_DISCONNECT_PLAYER;
                      
		        if (user_type == (int) NetworkConstants.CONNECTION_SERVER)
		        {
			        for (int cnt = 0 ; cnt < client_list.size(); cnt++)
			        {
                                    try
                                    {
                                        client_list.get(cnt).socket.getOutputStream( ).write(data_buffer,0,512);
                                        client_list.get(cnt).socket.getOutputStream().flush();
                                    }
                                    catch(IOException ex)
                                    {
                                        
                                    }
			        }
		        }
		        else if (user_type == (int) NetworkConstants.CONNECTION_CLIENT)
                        {
                            try
                            {
                                basePlayer.socket.getOutputStream().write(data_buffer,0,512);
                                basePlayer.socket.getOutputStream().flush();
                            }
                            catch(IOException ex)
                            {
                                
                            }
		        }

		        isNetworkReady = false;
		        try { basePlayer.socket.close(); } catch(IOException eax) { }
	        }
        }

        static void LoadString(byte[] buffer , int startIndex , String src)
        {
            int cnt = 0;

	        for (; cnt < src.length(); cnt++)
	        {
		        buffer[startIndex + cnt] = (byte) src.toCharArray()[cnt];
	        }

            buffer[startIndex + cnt] = 0;
        } 
        
        static byte[] GetBytes(int val)
        {
            return ByteBuffer.allocate(4).putInt(val).array();
        } 
                
        static int ToInt32(byte[] buffer , int startIndex)
        {
            return ByteBuffer.wrap(buffer, startIndex, 4).getInt();
        }
        
        static void LoadInteger(byte[] buffer , int startIndex , int value)
        {
            byte[] bytes = GetBytes(value);

	    for (int cnt = 0; cnt < 4; cnt++) 
            { 
                buffer[startIndex + cnt] = bytes[cnt]; 
            }
        }
        
        public static void createGameObject(GameObject_Scene gameObject)
        {
	        if (!isNetworkReady) return;

	        byte[] data_buffer = new byte[512];

	        data_buffer[0] = BUFFER_CREATE_GAMEOBJECT;
	        LoadString(data_buffer,1,gameObject.instance_name);
	        LoadString(data_buffer,2 + gameObject.instance_name.length(), gameObject.obj_instance.name);
	        LoadInteger(data_buffer,3 + gameObject.instance_name.length() + gameObject.obj_instance.name.length(),gameObject.pos_x);
	        LoadInteger(data_buffer,7 + gameObject.instance_name.length() + gameObject.obj_instance.name.length(),gameObject.pos_y);
	        LoadInteger(data_buffer,11 + gameObject.instance_name.length() + gameObject.obj_instance.name.length() ,gameObject.depth);

	        if (user_type == (int) NetworkConstants.CONNECTION_CLIENT)
	        {
                    try
                    {
                        basePlayer.socket.getOutputStream().write(data_buffer,0,512);
                        basePlayer.socket.getOutputStream().flush();
                    }
                    catch(IOException ex)
                    {
                        
                    }
	        }
	        else if (user_type == (int) NetworkConstants.CONNECTION_SERVER)
	        {
		        for (int cnt = 0; cnt < client_list.size(); cnt++)
		        {
                            try
                            {
			        client_list.get(cnt).socket.getOutputStream().write( data_buffer , 0 , 512 );
                                client_list.get(cnt).socket.getOutputStream().flush();
                            }
                            catch(IOException ex)
                            {
                                
                            }
		        }
	        }
        }

        public static void updateGameObject(GameObject_Scene gameObject)
        {
	        if (!isNetworkReady) return;

	         byte[] data_buffer = new byte[512];

	        data_buffer[0] = BUFFER_UPDATE_GAMEOBJECT;
	        LoadString(data_buffer,1,gameObject.instance_name);
	        LoadString(data_buffer,2 + gameObject.instance_name.length(), gameObject.obj_instance.name);
	        LoadInteger(data_buffer,3 + gameObject.instance_name.length() + gameObject.obj_instance.name.length(),gameObject.pos_x);
	        LoadInteger(data_buffer,7 + gameObject.instance_name.length() + gameObject.obj_instance.name.length(),gameObject.pos_y);
	        LoadInteger(data_buffer,11 + gameObject.instance_name.length() + gameObject.obj_instance.name.length() ,gameObject.depth);
	        data_buffer[15 + gameObject.instance_name.length() + gameObject.obj_instance.name.length()] = (gameObject.isDestroyed) ? (byte) 1 : (byte) 0;

	        if (user_type == (int) NetworkConstants.CONNECTION_CLIENT)
	        {
		    try
                    {
                        basePlayer.socket.getOutputStream().write(data_buffer,0,512);
                        basePlayer.socket.getOutputStream().flush();
                    }
                    catch(IOException ex)
                    {
                        
                    }
	        }
	        else if (user_type == (int) NetworkConstants.CONNECTION_SERVER)
	        {
		     for (int cnt = 0; cnt < client_list.size(); cnt++)
		        {
                            try
                            {
			        client_list.get(cnt).socket.getOutputStream().write( data_buffer , 0 , 512 );
                                client_list.get(cnt).socket.getOutputStream().flush();
                            }
                            catch(IOException ex)
                            {
                                
                            }
		        }
	        }
        }

        public static void sendMessage(String message, int flag)
        {
	        if (!isNetworkReady) return;

	        byte[] data_buffer = new byte[512];

	        data_buffer[0] = BUFFER_MESSAGE;
	        LoadInteger(data_buffer,1,message.length());
	        data_buffer[message.length() + 5] = (byte) flag;
	        LoadString(data_buffer,message.length() + 6,message);

	        if (user_type == (int) NetworkConstants.CONNECTION_CLIENT)
	        {
                    try
                    {
                        basePlayer.socket.getOutputStream().write(data_buffer,0,512);
                        basePlayer.socket.getOutputStream().flush();
                    }
                    catch(IOException ex)
                    {
                        
                    }
	        }
	        else if (user_type == (int) NetworkConstants.CONNECTION_SERVER)
	        {
                     for (int cnt = 0; cnt < client_list.size(); cnt++)
		        {
                            try
                            {
			        client_list.get(cnt).socket.getOutputStream().write( data_buffer , 0 , 512 );
                                client_list.get(cnt).socket.getOutputStream().flush();
                            }
                            catch(IOException ex)
                            {
                                
                            }
		        }
	        }
        }
}
