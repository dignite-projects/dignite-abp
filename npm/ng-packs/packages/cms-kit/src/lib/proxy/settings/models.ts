
export interface BrandSettingsDto {
  appName?: string;
  logoUrl?: string;
  logoReverseUrl?: string;
}

export interface SiteLanguageSettingsDto {
  defaultLanguage?: string;
  allLanguages: string[];
}

export interface UpdateBrandSettingsInput {
  appName: string;
  logoBlobName?: string;
  logoReverseBlobName?: string;
}

export interface UpdateSiteLanguageSettingsInput {
  defaultLanguage: string;
  allLanguages: string[];
}
