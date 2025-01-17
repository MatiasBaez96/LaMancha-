﻿using System;
using System.Collections.Generic;
using LaMancha.Services;
using LaMancha.Casting;
using LaMancha.Scripting;
using Raylib_cs;


namespace LaMancha
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the cast
            Dictionary<string, List<Actor>> cast = new Dictionary<string, List<Actor>>();

            // Bricks
            cast["player"] = new List<Actor>();
            Player player1 = new Player(Constants.PLAYER_HEIGHT, Constants.PLAYER_WIDTH, Constants.PLAYER_X, Constants.PLAYER_Y, "player1");
            cast["player"].Add(player1);

            // TODO: Add your bricks here

            // The Ball (or balls if desired)
            cast["player2"] = new List<Actor>();
            Player player2 = new Player(Constants.PLAYER_HEIGHT, Constants.PLAYER_WIDTH, 1425, Constants.PLAYER_Y, "player2");
            cast["player2"].Add(player2);

            // TODO: Add your ball here

            // The paddle
            cast["platform"] = new List<Actor>();
            Platform floor = new Platform(24, Constants.MAX_X, new Point(0,Constants.MAX_Y - 100));
            cast["platform"].Add(floor);
            Platform center = new Platform(24, 150, new Point(Constants.MAX_X / 2,630));
            cast["platform"].Add(center);
            Platform rightSide = new Platform(24, 150, new Point(Constants.MAX_X - 150,630));
            cast["platform"].Add(rightSide);
            // TODO: Add your paddle here

            // Create the script
            Dictionary<string, List<Action>> script = new Dictionary<string, List<Action>>();

            OutputService outputService = new OutputService();
            InputServiceP1 inputServiceP1 = new InputServiceP1();
            InputServiceP2 inputServiceP2 = new InputServiceP2();
            PhysicsService physicsService = new PhysicsService();
            AudioService audioService = new AudioService();

            script["output"] = new List<Action>();
            script["input"] = new List<Action>();
            script["update"] = new List<Action>();

            DrawActorsAction drawActorsAction = new DrawActorsAction(outputService); //conector que recibe un "objeto del tipo OutputSerivice"
            script["output"].Add(drawActorsAction);

            MoveActorsAction moveActorsAction = new MoveActorsAction();
            script["update"].Add(moveActorsAction);

            ControlActorsActionP1 controlActorsActionP1 = new ControlActorsActionP1(inputServiceP1);
            script["input"].Add(controlActorsActionP1);

            ControlActorsActionP2 controlActorsActionP2 = new ControlActorsActionP2(inputServiceP2);
            script["input"].Add(controlActorsActionP2);

            HandleCollisionsAction handleCollisionsAction = new HandleCollisionsAction(physicsService);
            script["update"].Add(handleCollisionsAction);
            
            // Start up the game
            outputService.OpenWindow(Constants.MAX_X, Constants.MAX_Y, "La Mancha", Constants.FRAME_RATE);
            audioService.StartAudio();
            audioService.PlaySound(Constants.SOUND_START);

            Director theDirector = new Director(cast, script);
            theDirector.Direct();

            audioService.StopAudio();
        }
        
    }
}
