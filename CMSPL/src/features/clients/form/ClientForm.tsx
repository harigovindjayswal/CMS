import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import type { FormEvent } from "react";
import { useClients } from "../../../lib/hooks/useClients";
import { useNavigate, useParams } from "react-router";

export default function ClientForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { updateClient, createClient, client, isClientLoading } = useClients(
    Number(id)
  );
  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const data: { [key: string]: FormDataEntryValue } = {};
    formData.forEach((value, key) => {
      data[key] = value;
    });

    if (client) {
      data.clientId = client.clientId.toString();
      // Convert types
      const payload: Client = {
        clientId: Number(data.clientId),
        userId: String(data.userId),
        firstName: String(data.firstName),
        middleName: String(data.middleName),
        lastName: String(data.lastName),
        emailId: String(data.emailId),
        mobileNo: String(data.mobileNo),
        address: String(data.address),
        state: Number(data.state),
        district: Number(data.district),
        city: Number(data.city),
        pinCode: String(data.pinCode),
        notes: String(data.notes),
        updatedBy: String(data.updatedBy),
        updatedDate: String(data.updatedDate),
        createdBy: String(data.createdBy),
        createdDate: String(data.createdDate),
        isActive: data.isActive === "true",
      };

      await updateClient.mutateAsync(payload as Client);
      navigate(`/clientDetails/${client.clientId}`);
    } else {
      const payload: Client = {
        clientId: Number(data.clientId),
        userId: String(data.userId),
        firstName: String(data.firstName),
        middleName: String(data.middleName),
        lastName: String(data.lastName),
        emailId: String(data.emailId),
        mobileNo: String(data.mobileNo),
        address: String(data.address),
        state: Number(data.state),
        district: Number(data.district),
        city: Number(data.city),
        pinCode: String(data.pinCode),
        notes: String(data.notes),
        updatedBy: String(data.updatedBy),
        updatedDate: String(data.updatedDate),
        createdBy: String(data.createdBy),
        createdDate: String(data.createdDate),
        isActive: data.isActive === "true",
      };
      await createClient.mutate(payload as Client, {
        onSuccess: (id) => {
          navigate(`/clientDetails/${id}`);
        },
      });
    }
  };
  if (isClientLoading) return <Typography>Loading...</Typography>;
  return (
    <Paper sx={{ borderRadius: 3, padding: 3 }}>
      <Typography variant="h5" gutterBottom color="primary">
        {client ? 'Edit Client' : 'Create Client'}
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit}
        display="flex"
        flexDirection="column"
        gap={3}
      >
        <TextField
          name="userId"
          label="User Id"
          defaultValue={client?.userId}
        />
        <TextField
          name="firstName"
          label="First Name"
          defaultValue={client?.firstName}
        />
        <TextField
          name="middleName"
          label="Middle Name"
          defaultValue={client?.middleName}
        />
        <TextField
          name="lastName"
          label="Last Name"
          defaultValue={client?.lastName}
        />
        <TextField
          name="emailId"
          label="Email"
          defaultValue={client?.emailId}
        />
        <TextField
          name="mobileNo"
          label="Mobile"
          defaultValue={client?.mobileNo}
        />
        <TextField
          name="address"
          label="Address"
          multiline
          rows={3}
          defaultValue={client?.address}
        />
        <TextField name="state" label="State" defaultValue={client?.state} />
        <TextField
          name="district"
          label="District"
          defaultValue={client?.district}
        />
        <TextField name="city" label="City" defaultValue={client?.city} />
        <TextField
          name="pinCode"
          label="Pin Code"
          defaultValue={client?.pinCode}
        />
        <TextField
          name="notes"
          label="Notes"
          multiline
          rows={3}
          defaultValue={client?.notes}
        />
        <TextField
          name="createdBy"
          label="Created By"
          defaultValue={client?.createdBy}
        />
        <TextField
          name="createdDate"
          label="Created Date"
          type="date"
          defaultValue={
            client?.createdDate
              ? new Date(client?.createdDate).toISOString().split("T")[0]
              : new Date().toISOString().split("T")[0]
          }
        />
        <TextField
          name="updatedBy"
          label="Updated By"
          defaultValue={client?.updatedBy}
        />
        <TextField
          name="updatedDate"
          label="Updated Date"
          type="date"
          defaultValue={
            client?.updatedDate
              ? new Date(client?.updatedDate).toISOString().split("T")[0]
              : new Date().toISOString().split("T")[0]
          }
        />
        <TextField
          name="isActive"
          label="Is Active"
          defaultValue={client?.isActive}
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
