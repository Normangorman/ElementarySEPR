using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTest;

[IntegrationTest.DynamicTestAttribute("testing_scene")]
// [IntegrationTest.Ignore]
[IntegrationTest.SucceedWithAssertions]
[IntegrationTest.TimeoutAttribute(2)]
public class PlayerIntegration : MonoBehaviour
{
    public void Start()
    {
        IntegrationTest.Pass(gameObject);
        UnityTest.Assertions.Equals(1, 2);
    }
}
