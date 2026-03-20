import { z } from "zod";
import { requiredString } from "../util/util";

export const lawyerRequestSchema = z.object({
  caseTypeId: requiredString("Case type"),
  stateId: requiredString("State"),
  districtId: requiredString("District"),
  cityId: requiredString("City"),
  lawyerId: requiredString("Lawyer"),
  caseDescription: requiredString("Case description").max(2000),
});

export type LawyerRequestSchema = z.input<typeof lawyerRequestSchema>;

