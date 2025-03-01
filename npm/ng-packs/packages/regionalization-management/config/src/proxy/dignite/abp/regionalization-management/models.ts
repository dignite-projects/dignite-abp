
export interface RegionalizationDto {
  defaultCultureName?: string;
  availableCultureNames: string[];
}

export interface UpdateRegionalizationInput {
  defaultCultureName: string;
  availableCultureNames: string[];
}
