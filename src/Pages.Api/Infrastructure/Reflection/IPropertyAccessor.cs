// ==========================================================================
//  IPropertyAccessor.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

namespace Pages.Api.Infrastructure.Reflection
{
    public interface IPropertyAccessor
    {
        object Get(object target);

        void Set(object target, object value);
    }
}
