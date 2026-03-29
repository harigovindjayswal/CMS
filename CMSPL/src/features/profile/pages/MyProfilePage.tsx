import { Box, Button, Paper, Typography } from "@mui/material";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import TextInput from "../../../app/shared/components/TextInput";
import SelectInput from "../../../app/shared/components/SelectInput";
import { useProfile } from "../../../lib/hooks/useProfile";
import { useUtility } from "../../../lib/hooks/useUtility";
import {
  clientProfileSchema,
  type ClientProfileSchema,
} from "../../../lib/schemas/clientProfileSchema";
import {
  lawyerProfileSchema,
  type LawyerProfileSchema,
} from "../../../lib/schemas/lawyerProfileSchema";
import {
  staffProfileSchema,
  type StaffProfileSchema,
} from "../../../lib/schemas/staffProfileSchema";

function ClientProfileForm() {
  const { clientProfileQuery, saveClientProfile } = useProfile();

  const form = useForm<ClientProfileSchema>({
    mode: "onTouched",
    resolver: zodResolver(clientProfileSchema),
    defaultValues: {},
  });

  useEffect(() => {
    if (clientProfileQuery.data) form.reset(clientProfileQuery.data);
  }, [clientProfileQuery.data, form]);

  return (
    <Paper
      component="form"
      onSubmit={form.handleSubmit(async (data) => {
        await saveClientProfile.mutateAsync(data);
      })}
      sx={{ p: 3, borderRadius: 3, maxWidth: "md", mx: "auto" }}
    >
      <Typography variant="h5" fontWeight="bold">
        My profile (Client)
      </Typography>
      <Box sx={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: 2, mt: 2 }}>
        <TextInput label="First name" control={form.control} name="firstName" />
        <TextInput label="Last name" control={form.control} name="lastName" />
        <TextInput label="Email" control={form.control} name="emailId" />
        <TextInput label="Mobile" control={form.control} name="mobileNo" />
        <TextInput label="Address" control={form.control} name="address" />
        <TextInput label="City" control={form.control} name="city" />
      </Box>
      <Button
        sx={{ mt: 3 }}
        type="submit"
        variant="contained"
        size="large"
        disabled={!form.formState.isValid}
        loading={saveClientProfile.isPending}
      >
        Save
      </Button>
    </Paper>
  );
}

function LawyerProfileForm() {
  const { lawyerProfileQuery, saveLawyerProfile } = useProfile();
  const states = useUtility("State");

  const form = useForm<LawyerProfileSchema>({
    mode: "onTouched",
    resolver: zodResolver(lawyerProfileSchema),
    defaultValues: { stateId: " ", districtId: " ", cityId: " " },
  });

  const stateId = form.watch("stateId");
  const districtId = form.watch("districtId");
  const districts = useUtility(
    "District",
    stateId && stateId.trim() !== "" ? stateId : undefined
  );
  const cities = useUtility(
    "City",
    districtId && districtId.trim() !== "" ? districtId : undefined
  );

  useEffect(() => {
    if (!lawyerProfileQuery.data) return;
    form.reset({
      ...lawyerProfileQuery.data,
      stateId:
        lawyerProfileQuery.data.stateId != null
          ? String(lawyerProfileQuery.data.stateId)
          : " ",
      districtId: " ",
      cityId:
        lawyerProfileQuery.data.cityId != null
          ? String(lawyerProfileQuery.data.cityId)
          : " ",
      yearsOfExperience:
        lawyerProfileQuery.data.yearsOfExperience != null
          ? String(lawyerProfileQuery.data.yearsOfExperience)
          : "",
    });
  }, [lawyerProfileQuery.data, form]);

  return (
    <Paper
      component="form"
      onSubmit={form.handleSubmit(async (data) => {
        const rawYears = data.yearsOfExperience?.trim();
        const yearsOfExperience =
          rawYears && rawYears.length > 0 ? Number(rawYears) : null;
        const parsedStateId =
          data.stateId && data.stateId.trim() !== "" ? Number(data.stateId) : null;
        const parsedCityId =
          data.cityId && data.cityId.trim() !== "" ? Number(data.cityId) : null;

        await saveLawyerProfile.mutateAsync({
          firstName: data.firstName,
          middleName: data.middleName,
          lastName: data.lastName,
          dateOfBirth: data.dateOfBirth ?? null,
          emailId: data.emailId,
          mobileNo: data.mobileNo,
          address: data.address,
          stateId: parsedStateId,
          cityId: parsedCityId,
          barLicenseNumber: data.barLicenseNumber,
          yearsOfExperience,
          courtDetails: data.courtDetails,
        });
      })}
      sx={{ p: 3, borderRadius: 3, maxWidth: "md", mx: "auto" }}
    >
      <Typography variant="h5" fontWeight="bold">
        My profile (Lawyer)
      </Typography>
      <Box sx={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: 2, mt: 2 }}>
        <TextInput label="First name" control={form.control} name="firstName" />
        <TextInput label="Last name" control={form.control} name="lastName" />
        <TextInput label="Email" control={form.control} name="emailId" />
        <TextInput label="Mobile" control={form.control} name="mobileNo" />
        <TextInput label="Address" control={form.control} name="address" />
        <SelectInput
          label="State"
          control={form.control}
          name="stateId"
          items={states.optionLoader}
        />
        <SelectInput
          label="District"
          control={form.control}
          name="districtId"
          items={districts.optionLoader}
        />
        <SelectInput
          label="City"
          control={form.control}
          name="cityId"
          items={cities.optionLoader}
        />
        <TextInput
          label="Bar license number"
          control={form.control}
          name="barLicenseNumber"
        />
        <TextInput
          label="Years of experience"
          control={form.control}
          name="yearsOfExperience"
        />
        <TextInput label="Court details" control={form.control} name="courtDetails" />
      </Box>
      <Button
        sx={{ mt: 3 }}
        type="submit"
        variant="contained"
        size="large"
        disabled={!form.formState.isValid}
        loading={saveLawyerProfile.isPending}
      >
        Save
      </Button>
    </Paper>
  );
}

function StaffProfileForm() {
  const { staffProfileQuery, saveStaffProfile } = useProfile();

  const form = useForm<StaffProfileSchema>({
    mode: "onTouched",
    resolver: zodResolver(staffProfileSchema),
    defaultValues: { lawyerId: 0 },
  });

  useEffect(() => {
    if (staffProfileQuery.data) form.reset(staffProfileQuery.data);
  }, [staffProfileQuery.data, form]);

  return (
    <Paper
      component="form"
      onSubmit={form.handleSubmit(async (data) => {
        await saveStaffProfile.mutateAsync(data);
      })}
      sx={{ p: 3, borderRadius: 3, maxWidth: "md", mx: "auto" }}
    >
      <Typography variant="h5" fontWeight="bold">
        My profile (Staff)
      </Typography>
      <Box sx={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: 2, mt: 2 }}>
        <TextInput label="First name" control={form.control} name="firstName" />
        <TextInput label="Last name" control={form.control} name="lastName" />
        <TextInput label="Email" control={form.control} name="emailId" />
        <TextInput label="Mobile" control={form.control} name="mobileNo" />
        <TextInput label="Address" control={form.control} name="address" />
        <TextInput label="Assigned Lawyer ID" control={form.control} name="lawyerId" disabled />
      </Box>
      <Button
        sx={{ mt: 3 }}
        type="submit"
        variant="contained"
        size="large"
        disabled={!form.formState.isValid}
        loading={saveStaffProfile.isPending}
      >
        Save
      </Button>
    </Paper>
  );
}

export default function MyProfilePage() {
  const { userType } = useProfile();

  if (!userType) return <Typography>Loading...</Typography>;
  if (userType === "Client") return <ClientProfileForm />;
  if (userType === "Lawyer") return <LawyerProfileForm />;
  if (userType === "Staff") return <StaffProfileForm />;

  return (
    <Paper sx={{ p: 3, borderRadius: 3 }}>
      <Typography variant="h5">My profile</Typography>
      <Typography sx={{ mt: 1 }}>
        Profile management is currently available for Client, Lawyer, and Staff accounts.
      </Typography>
    </Paper>
  );
}
