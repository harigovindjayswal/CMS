import { z } from "zod";
import { requiredString } from "../util/util";

export const clientProfileSchema = z.object({
  firstName: requiredString("First name").max(50),
  middleName: z.string().trim().max(50).optional(),
  lastName: requiredString("Last name").max(50),
  emailId: requiredString("Email").email(),
  mobileNo: requiredString("Mobile").max(20),
  address: requiredString("Address").max(255),
  state: z.number().nullable().optional(),
  district: z.number().nullable().optional(),
  city: z.string().trim().max(100).optional(),
  pinCode: z.string().trim().max(6).optional(),
  notes: z.string().trim().max(500).optional(),
});

export type ClientProfileSchema = z.input<typeof clientProfileSchema>;

