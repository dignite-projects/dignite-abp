﻿using System.Collections.Generic;
using Volo.Abp.Localization;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Components.LanguageSwitch;

public class LanguageSwitchViewComponentModel
{
    public LanguageInfo CurrentLanguage { get; set; }

    public List<LanguageInfo> OtherLanguages { get; set; }
}