using NUnit.Framework;
using SimpleCore.Extensions;
using UnityEngine;
using UnityAssert = UnityEngine.Assertions.Assert;

/// <summary>
/// 测试 Unity Component 扩展类的类。
/// </summary>
public class TestComponentExtensions
{
    private Component _transform;//使用的组件

    [SetUp]
    public void Setup()
    {
        _transform = new GameObject("TestComponentExtensions").transform;//创建组件
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(_transform.gameObject);//销毁组件
    }

    /// <summary>
    /// 测试获取或添加组件。
    /// </summary>
    [Test]
    public void GetOrAddComponent()
    {
        var light = _transform.GetOrAddComponent<Light>();
        //1.判断灯光组件是否添加成功
        UnityAssert.IsNotNull(light);
        var boxCollider = _transform.GetOrAddComponent(typeof(BoxCollider));
        //2.判断BoxCollider组件是否添加成功
        UnityAssert.IsNotNull(boxCollider);
            
        //销毁组件
        Object.Destroy(light);
        Object.Destroy(boxCollider);
    }

    /// <summary>
    /// 测试设置组件绑定的GameObject的状态。
    /// </summary>
    [Test]
    public void SetActive()
    {
        _transform.SetActive(false);
        //1.判断隐藏物体是否成功
        Assert.IsFalse(_transform.gameObject.activeSelf);
        _transform.SetActive(true);
        //2.判断显示物体是否成功
        Assert.IsTrue(_transform.gameObject.activeSelf);
    }

}