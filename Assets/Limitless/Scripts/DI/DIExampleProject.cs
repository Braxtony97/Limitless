using UnityEngine;

namespace DI
{
    public class MyProjectServiceExample { }

    public class MySceneServiceExample
    {
        private readonly MyProjectServiceExample _projectServiceExample;

        public MySceneServiceExample(MyProjectServiceExample projectServiceExample)
        {
            _projectServiceExample = projectServiceExample;
        }
    }

    public class MyFactory
    {
        public MyObjectExapmle CreateInstance(string id, int par)
        {
            return new MyObjectExapmle (id, par);
        }
    }

    public class MyObjectExapmle
    {
        private readonly string _id;
        private readonly int _par;

        public MyObjectExapmle(string id, int par)
        {
            _id = id;
            _par = par;
        }
    }
    
    public class DIExampleProject : MonoBehaviour
    {
        private void Awake()
        {
            var projectContainer = new DIContainer();
            
            projectContainer.RegisterSingleton<MyProjectServiceExample>(c => new MyProjectServiceExample());
            projectContainer.RegisterSingleton("Option 1", c => new MyProjectServiceExample());
            projectContainer.RegisterSingleton("Option 2", c => new MyProjectServiceExample());
            
            // scene change

            var sceneRoot = FindObjectOfType<DIExampleScene>();
            sceneRoot.Init(projectContainer);
        } 
    }
}