
export interface CreateUpdateRatingInput {
  starCount: number;
}

export interface RatingDto {
  id?: string;
  entityType?: string;
  entityId?: string;
  starCount: number;
  creatorId?: string;
  creationTime?: string;
}

export interface RatingWithStarCountDto {
  starCount: number;
  count: number;
  isSelectedByCurrentUser: boolean;
}
