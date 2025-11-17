import { Grid2 } from "@mui/material";
import ClientList from "./ClientList";

export default function ClientDashboard() {
  return (
    <>
      <Grid2 container spacing={3}>
        <Grid2 size={7}>      
           <ClientList  />       
        </Grid2>
        <Grid2 size={5}>
          Client filter here...
        </Grid2>
      </Grid2>
    </>
  );
}
