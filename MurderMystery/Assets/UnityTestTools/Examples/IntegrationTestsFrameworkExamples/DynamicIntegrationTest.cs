using System;
using System.Collections.Generic;
using UnityEngine;

[IntegrationTest.DynamicTestAttribute("testing_scene")]
// [IntegrationTest.Ignore]
[IntegrationTest.ExpectExceptions(false, typeof(ArgumentException))]
[IntegrationTest.SucceedWithAssertions]
[IntegrationTest.TimeoutAttribute(1)]
[IntegrationTest.ExcludePlatformAttribute(RuntimePlatform.Android, RuntimePlatform.LinuxPlayer)]
public class DynamicIntegrationTest : MonoBehaviour
{
    public void Start()
    {
        IntegrationTest.Pass(gameObject);
    }
}
