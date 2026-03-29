import { z } from "zod";
import { requiredString } from "../util/util";

export const staffProfileSchema = z.object({
  firstName: requiredString("First name").max(50),
  middleName: z.string().max(50).optional().or(z.literal("")),
  lastName: requiredString("Last name").max(50),
  dateOfBirth: z.string().optional().nullable(),
  emailId: z.string().email(),
  mobileNo: requiredString("Mobile").max(20),
  address: z.string().max(255).optional().or(z.literal("")),
  lawyerId: z.number().int().positive(),
});

export type StaffProfileSchema = z.input<typeof staffProfileSchema>;

