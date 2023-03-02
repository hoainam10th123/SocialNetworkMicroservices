export interface UserLocation{
    userName: string;
    distance: string;
    imageUrl: string;
}

export interface UsersNearestStore{
    resultUsers: UserLocation[]
}