import {
  Grid2,
  Typography,
} from "@mui/material";
import { useParams } from "react-router";
import { useClients } from "../../../lib/hooks/useClients";
import ClientDetailsHeader from "./ClientDetailsHeader";
import ClientDetailsInfo from "./ClientDetailsInfo";
import ClientDetailsChats from "./ClientDetailsChats";
import ClientDetailsSideBar from "./ClientDetailsSideBar";

export default function ClientDetailsPage() {
  const { id } = useParams();
  const { client, isClientLoading } = useClients(Number(id));

  if (isClientLoading) return <Typography>Loading...</Typography>;
  if (!client) return <Typography>Client Not Found</Typography>;
  return (
    <Grid2 container spacing={3}>
      <Grid2 size={8}>
        <ClientDetailsHeader client={client} />
        <ClientDetailsInfo client={client} />
        <ClientDetailsChats />
      </Grid2>
      <Grid2 size={4}>
        <ClientDetailsSideBar />
      </Grid2>
    </Grid2>
  );
}
