using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class GameStateObserver : IGameState
    {
        private static GameStateObserver gameStateObserver;
        private List<IGameState> entities;

        public static GameStateObserver getInstance()
        {
            return gameStateObserver ??= new GameStateObserver();
        }

        public GameStateObserver()
        {
            entities ??= new List<IGameState>();
        }

        public void Add(IGameState entity)
        {
            entities.Add(entity);
        }

        public void Remove(IGameState entity)
        {
            entities.Remove(entity);
        }


        public void OnReady()
        {
            foreach (var e in entities)
            {
                e.OnReady();
            }
        }

        public void OnActive()
        {
            foreach (var e in entities)
            {
                e.OnActive();
            }
        }

        public void OnHit()
        {
            foreach (var e in entities)
            {
                e.OnHit();
            }
        }

        public void OnGameOver()
        {
            foreach (var e in entities)
            {
                e.OnGameOver();
            }
        }
    }
}