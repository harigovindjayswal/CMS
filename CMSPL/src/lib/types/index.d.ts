type Client = {
  clientId?: number;
  userId?: string;
  firstName: string;
  middleName: string;
  lastName: string;
  emailId: string;
  mobileNo: string;
  address: string;
  state: number;
  district: number;
  city: string;
  pinCode: string;
  notes: string;
  updatedBy?: string;
  updatedDate?: string;
  createdBy?: string;
  createdDate?: string;
  isActive?: boolean;
};

type User = {
  id: string;
  email: string;
  displayName: string;
  image?: string;
  role?: string;
  userType: string;
};

type OptionLoader = {
  id: string;
  name: string;
};

type ClientProfile = {
  firstName?: string;
  middleName?: string;
  lastName?: string;
  emailId?: string;
  mobileNo?: string;
  address?: string;
  state?: number | null;
  district?: number | null;
  city?: string;
  pinCode?: string;
  notes?: string;
};

type LawyerProfile = {
  firstName?: string;
  middleName?: string;
  lastName?: string;
  dateOfBirth?: string | null;
  emailId?: string;
  mobileNo?: string;
  address?: string;
  stateId?: number | null;
  cityId?: number | null;
  barLicenseNumber?: string;
  yearsOfExperience?: number | null;
  courtDetails?: string;
};

type LawyerRequestStatus = "Pending" | "Read" | "Accepted" | "Rejected" | number;

type LawyerRequest = {
  lawyerRequestId: number;
  clientId: number;
  clientName?: string;
  lawyerId: number;
  lawyerName?: string;
  caseTypeId: number;
  caseTypeName?: string;
  stateId: number;
  districtId: number;
  cityId: number;
  caseDescription: string;
  status: LawyerRequestStatus;
  lawyerRemark?: string;
};

type CaseItem = {
  caseId: number;
  clientId: number;
  clientName?: string;
  lawyerRequestId?: number | null;
  title: string;
  description?: string;
  caseType?: string;
  courtName?: string;
  caseNumber?: string;
  purpose?: string;
  filingDate?: string | null;
  status: string;
  stage: string;
  createdAt?: string | null;
  updatedAt?: string | null;
};

type CaseNote = {
  noteId: number;
  userId: string;
  content?: string;
  isPrivate?: boolean | null;
  createdAt?: string | null;
};

type CaseDocument = {
  documentId: number;
  title?: string;
  category?: string;
  uploadedAt?: string | null;
  uploadedBy: string;
};

type CaseDetails = CaseItem & {
  notes: CaseNote[];
  documents: CaseDocument[];
};

type StateMst = {
  stateId: number;
  name: string;
  isActive: boolean;
};

type DistrictMst = {
  districtId: number;
  stateId: number;
  name: string;
  isActive: boolean;
};

type CityMst = {
  cityId: number;
  districtId: number;
  name: string;
  isActive: boolean;
};

type CaseTypeMst = {
  caseTypeId: number;
  typeName: string;
  isActive: boolean;
};

type CourtTypeMst = {
  courtTypeId: number;
  typeName: string;
  isActive: boolean;
};

type CourtMst = {
  courtId: number;
  courtTypeId: number;
  cityId: number;
  name: string;
  isActive: boolean;
};

type ManagedClient = {
  clientId: number;
  userId?: string | null;
  emailId?: string | null;
  firstName?: string | null;
  lastName?: string | null;
  mobileNo?: string | null;
  isActive?: boolean | null;
  createdDate?: string | null;
};

type ManagedLawyer = {
  lawyerId: number;
  userId: string;
  emailId?: string | null;
  firstName?: string | null;
  lastName?: string | null;
  mobileNo?: string | null;
  stateId?: number | null;
  cityId?: number | null;
  isActive: boolean;
  createdDate?: string | null;
};

type ManagedCase = {
  caseId: number;
  clientId: number;
  clientName?: string | null;
  assignedLawyerUserId?: string | null;
  assignedLawyerName?: string | null;
  title: string;
  caseType?: string | null;
  courtName?: string | null;
  caseNumber?: string | null;
  status: string;
  stage: string;
  createdAt?: string | null;
  updatedAt?: string | null;
};
