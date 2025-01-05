using UnityEngine;

namespace DI
{
    public class DIExampleScene : MonoBehaviour
    {
        public void Init(DIContainer projectContainer)
        {
            // var serviceWithoutTag = projectContainer.Resolve<MyProjectServiceExample>();
            // var serviceWithTag1 = projectContainer.Resolve<MyProjectServiceExample>("Option 1");
            // var serviceWithTag2 = projectContainer.Resolve<MyProjectServiceExample>("Option 2");
            
            var sceneContainer = new DIContainer(projectContainer);
            sceneContainer.RegisterSingleton(c => new MySceneServiceExample(c.Resolve<MyProjectServiceExample>()));
            sceneContainer.RegisterSingleton(c => new MyFactory());
            sceneContainer.RegisterInstance(new MyObjectExapmle("instance", 10));
            
            var objectFactory = sceneContainer.Resolve<MyFactory>();
            
            var instance = sceneContainer.Resolve<MyObjectExapmle>();
        }
    }
}