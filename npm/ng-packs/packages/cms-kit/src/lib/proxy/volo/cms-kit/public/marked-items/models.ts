
export interface MarkedItemDto {
  iconName?: string;
}

export interface MarkedItemWithToggleDto {
  markedItem: MarkedItemDto;
  isMarkedByCurrentUser: boolean;
}
