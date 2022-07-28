using System;
using NUnit.Framework;
using SimpleCore.Extensions;
using UnityEngine;

/// <summary>
/// 测试 Unity Color 扩展类的类。
/// </summary>
public class TestColorExtensions
{
    /// <summary>
    ///     测试 Color 的赋值扩展方法。
    /// </summary>
    [Test]
    public void ColorSetValue()
    {
        const float r = 0.2f, g = 0.3f, b = 0.4f, a = 0.5f;
        var color = Color.black;
        //1.判断是否抛出异常
        Assert.Throws<ArgumentNullException>(() => color.SetValue());
        //2.判断赋值后的值是否正确
        color = color.SetValue(r, g, b, a);
        Assert.AreEqual(color.r, r);
        Assert.AreEqual(color.g, g);
        Assert.AreEqual(color.b, b);
        Assert.AreEqual(color.a, a);
    }

    /// <summary>
    ///     测试 Color32 的赋值扩展方法。
    /// </summary>
    [Test]
    public void Color32SetValue()
    {
        const byte r = 20, g = 50, b = 70, a = 100;
        var color32 = new Color32(0, 0, 0, 255);
        //1.判断是否抛出异常
        Assert.Throws<ArgumentNullException>(() => color32.SetValue());
        //2.判断赋值后的值是否正常
        color32 = color32.SetValue(r, g, b, a);
        Assert.AreEqual(color32.r, r);
        Assert.AreEqual(color32.g, g);
        Assert.AreEqual(color32.b, b);
        Assert.AreEqual(color32.a, a);
    }
}