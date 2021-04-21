import { Photo } from "./Photo";

    export interface Member {
        id: number;
        username: string;
        photoUrl: string;
        age: number;
        passwordHash: string;
        passwordSalt: string;
        knownAs: string;
        gender: string;
        introduction: string;
        lookingFor: string;
        interests: string;
        city: string;
        country: string;
        created: Date;
        lastActive: Date;
        photos: Photo[];
    }