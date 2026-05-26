using System;
using System.Collections.Generic;

public class SceneManager
{
    private Stack<IScene> sceneStack;

    public SceneManager()
    {
        sceneStack = new();
    }

    public void AddScene(IScene scene)
    {
        scene.Load();
        sceneStack.Push(scene);
    }

    public void RemoveScene()
    {
        sceneStack.Pop();
    }

    public IScene GetCurrentScene()
    {
        return sceneStack.Peek();
    }
}