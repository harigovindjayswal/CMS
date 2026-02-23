import {email, z} from "zod";
import { requiredString } from "../util/util";

export const registerSchema = z.object({
    email: email(),
    displayName: requiredString('Display Name'),
    password: requiredString('Password'),
    userType:requiredString('User Type'),
});

export type RegisterSchema = z.input<typeof registerSchema>;