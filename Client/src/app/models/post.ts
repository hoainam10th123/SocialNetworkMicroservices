import { IComment } from "./comment";

export interface IPost{
    id: string;
    createdDate: Date;
    updatedDate: Date;
    title: string;
    noiDung: string;
    username: string;
    userPhotoUrl: string;
    comments: IComment[];
}