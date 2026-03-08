import {z} from 'zod';
import { requiredString } from '../util/util';

export const loginSchema = z.object({
    email: z.email({error: 'Invalid email address'}),
    password: requiredString('Password').min(6, {error: 'Password must be at least 6 characters'}),
    useCookies:z.boolean({error:'Invalid selection !'})
});

export type LoginSchema = z.input<typeof loginSchema>;