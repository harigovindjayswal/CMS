import { z } from "zod";
import { requiredString } from "../util/util";

export const lawyerProfileSchema = z.object({
  firstName: requiredString("First name").max(50),
  middleName: z.string().trim().max(50).optional(),
  lastName: requiredString("Last name").max(50),
  dateOfBirth: z.string().optional().nullable(),
  emailId: requiredString("Email").email(),
  mobileNo: requiredString("Mobile").max(20),
  address: requiredString("Address").max(255),
  stateId: z.string().trim().optional(),
  districtId: z.string().trim().optional(),
  cityId: z.string().trim().optional(),
  barLicenseNumber: requiredString("Bar license").max(80),
  yearsOfExperience: z
    .string()
    .trim()
    .regex(/^(|\d{1,2})$/, { message: "Enter 0-70" })
    .optional(),
  courtDetails: z.string().trim().max(400).optional(),
});

export type LawyerProfileSchema = z.input<typeof lawyerProfileSchema>;
