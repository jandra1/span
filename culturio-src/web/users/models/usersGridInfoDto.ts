import { UserDto } from "./userDto";

export interface IUsersGridInfoDto {
    totalCount: number;
    users: UserDto[];
}

export class UsersGridInfoDto implements IUsersGridInfoDto {
    public totalCount: number;
    public users: UserDto[];

    constructor(totalCount: number, users: UserDto[]) {
        this.totalCount = totalCount;
        this.users = users;
    }
}