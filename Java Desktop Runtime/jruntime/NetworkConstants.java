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
public class NetworkConstants 
{
    public static final int MESSAGE_FLAG_SERVER_ONLY = 0xA,
        MESSAGE_FLAG_EVERYONE = 0xB,
        CONNECTION_SERVER = 0xC,
        CONNECTION_CLIENT = 0xD,
        HANDLER_MESSAGE = 0x7,
        HANDLER_UPDATE_GAMEOBJECT = 0x10,
        HANDLER_DESTROY_GAMEOBJECT = 0x12,
        HANDLER_CREATE_GAMEOBJECT = 0x51,
        HANDLER_DISCONNECT_PLAYER = 0x61,
        HANDLER_CONNECTED = 0x30;
}
