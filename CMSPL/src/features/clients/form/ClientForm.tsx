import { Box, Button, Paper, Typography } from "@mui/material";
import { useClients } from "../../../lib/hooks/useClients";
import { useNavigate, useParams } from "react-router";
import { useForm } from "react-hook-form";
import { useEffect } from "react";
import {
  clientSchema,
  type ClientSchema,
} from "../../../lib/schemas/ClientSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import TextInput from "../../../app/shared/components/TextInput";
import SelectInput from "../../../app/shared/components/SelectInput";
import { categoryOptions } from "./categoryOptions";
import { toast } from "react-toastify";
export default function ClientForm() {
  const navigate = useNavigate();
  const { id } = useParams();
  const { updateClient, createClient, client, isClientLoading } = useClients(
    Number(id)
  );
  const { reset, control, handleSubmit } = useForm<ClientSchema>({
    mode: "onTouched",
    resolver: zodResolver(clientSchema) as any,
    defaultValues: {
      //userId: "",
      firstName: "",
      middleName: "",
      lastName: "",
      emailId: "",
      mobileNo: "",
      address: "",
      state: 0, // numeric → use 0 (or -1 if you prefer “not selected”)
      district: 0, // numeric → use 0
      city: "",
      pinCode: "",
      notes: "",
      // updatedBy: "",
      // updatedDate: "", // ISO string later
      // createdBy: "",
      // createdDate: "", // ISO string later

      //   isActive:true
    },
  });
  const onSubmit = async (data: ClientSchema) => {
    const { ...rest } = data;
    const flattenedData = { ...rest };
    try {
      if (client?.clientId) {
        updateClient.mutate(
          { ...client, ...flattenedData },
          {
            onSuccess: () => navigate(`/clientDetails/${client.clientId}`),
            onError: (error) => {
              if (Array.isArray(error)) {
                error.forEach((e) => toast.warning(e));
              } else {
                toast.warning(error?.message ?? "Something went wrong");
              }
            },
          }
        );
      } else {
        console.log(flattenedData);
        createClient.mutate(flattenedData, {
          onSuccess: (clientId) => navigate(`/clientDetails/${clientId}`),
          onError: (error) => {
            if (Array.isArray(error)) {
              error.forEach((e) => toast.warning(e));
            } else {
              toast.warning(error?.message ?? "Something went wrong");
            }
          },
        });
      }
    } catch (error) {
      console.log(error);
    }
  };
  useEffect(() => {
    if (client) reset(client);
  }, [client, reset]);

  if (isClientLoading) return <Typography>Loading...</Typography>;
  return (
    <Paper sx={{ borderRadius: 3, padding: 3 }}>
      <Typography variant="h5" gutterBottom color="primary">
        {client ? "Edit Client" : "Create Client"}
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit(onSubmit)}
        display="flex"
        flexDirection="column"
        gap={3}
      >
        <TextInput label="firstName" control={control} name="firstName" />
        <TextInput label="middleName" control={control} name="middleName" />
        <TextInput label="lastName" control={control} name="lastName" />

        <TextInput label="emailId" control={control} name="emailId" />
        <TextInput label="mobileNo" control={control} name="mobileNo" />
        <TextInput label="address" control={control} name="address" />
        {/* <TextInput label="state" control={control} name="state" />

        <TextInput label="district" control={control} name="district" /> */}
        <Box display="flex" gap={3}>
          <SelectInput
            items={categoryOptions}
            label="state"
            control={control}
            name="state"
          />
        </Box>
        <Box display="flex" gap={3}>
          <SelectInput
            items={categoryOptions}
            label="district"
            control={control}
            name="district"
          />
        </Box>
        <TextInput label="city" control={control} name="city" />
        <TextInput label="pinCode" control={control} name="pinCode" />
        <TextInput
          label="notes"
          control={control}
          name="notes"
          multiline
          rows={3}
        />

        <Box display="flex" justifyContent="end" gap={3}>
          <Button color="inherit" onClick={() => {}}>
            Cancel
          </Button>
          <Button
            type="submit"
            color="success"
            variant="contained"
            disabled={updateClient.isPending || createClient.isPending}
          >
            Submit
          </Button>
        </Box>
      </Box>
    </Paper>
  );
}
