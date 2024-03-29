﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Net.Http.Headers;
using MinimalApi.Auth.ApiKeyAuth;
using MinimalApi.Auth.BasicAuth;
using System;

namespace MinimalApi.Auth;

public static class DefaultAuthScheme
{
    public const string SchemeName = $"{ApiKeySchemeConstants.SchemeName}_OR_{BasicSchemeConstants.SchemeName}_OR_{NegotiateDefaults.AuthenticationScheme}";

    public static void ChooseAuthScheme(PolicySchemeOptions options)
    {
        options.ForwardDefaultSelector = context =>
        {
            string authorization = context.Request.Headers[HeaderNames.Authorization];
            string username = context.Request.Headers[BasicAuthHandler.UserNameHeader];
            if (!string.IsNullOrEmpty(authorization) && !authorization.Contains("Negotiate", StringComparison.OrdinalIgnoreCase))
            {
                return ApiKeySchemeConstants.SchemeName;
            }
            if (!string.IsNullOrEmpty(username))
            {
                return BasicSchemeConstants.SchemeName;
            }
            return NegotiateDefaults.AuthenticationScheme;
        };
    }
}