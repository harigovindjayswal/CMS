import { z } from "zod";

// const requiredString = (fieldName: string) =>
//   z
//     .string()
//     .optional()
//     .refine((val) => val !== undefined && val.trim().length > 0, {
//       message: `${fieldName} is required`,
//     });

// const requiredNumber = (fieldName: string) =>
//   z
//     .number()
//     .optional()
//     .refine((val) => val !== undefined && !isNaN(val), {
//       message: `${fieldName} is required`,
//     });



const requiredString = (fieldName: string) =>
  z.string().min(1, { message: `${fieldName} is required` });

// const requiredNumber = (fieldName: string) =>
//   z.number().refine((val) => !isNaN(val), { message: `${fieldName} is required` });

// ----------------------
// Required boolean
// ----------------------
// const requiredBoolean = (fieldName: string) =>
//   z.boolean().refine((val) => val !== undefined, { message: `${fieldName} is required` });

export const clientSchema = z.object({
  // clientId: z.number().optional(),

  // userId: requiredString("userId"),

  firstName: requiredString("firstName"),
  middleName: requiredString("middleName"),
  lastName: requiredString("lastName"),

  emailId: requiredString("emailId"),

  mobileNo: requiredString("mobileNo"),

  address: requiredString("address"),

  state: z.coerce.number().min(1, { message: "State is required" }),
  district: z.coerce.number().min(1, { message: "District is required" }),
  city: requiredString("city"),

  pinCode: requiredString("pinCode"),

  notes: requiredString("notes"),

  // updatedBy: requiredString("updatedBy"),
  // updatedDate: requiredString("updatedDate"), // can be z.coerce.date() if needed

  // createdBy: requiredString("createdBy"),
  // createdDate: requiredString("createdDate"), // can be z.coerce.date() if needed

  // isActive: z
  // .boolean()
  // .refine((val) => val !== undefined, {
  //   message: "Is Active is required",
  //})
});

export type ClientSchema = z.infer<typeof clientSchema>;
