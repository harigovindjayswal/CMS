import { Grid2 } from "@mui/material";
import ClientList from "./ClientList";
import ClientFilters from "./ClientFilters";

export default function ClientDashboard() {
  return (
    <>
      <Grid2 container spacing={3}>
        <Grid2 size={7}>      
           <ClientList  />       
        </Grid2>
        <Grid2 size={5}>
          <ClientFilters />
        </Grid2>
      </Grid2>
    </>
  );
}
