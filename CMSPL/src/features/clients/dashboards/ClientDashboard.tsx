import { Grid2 } from "@mui/material";
import ClientList from "./ClientList";
import ClientDetails from "../details/ClientDetails";
import ClientForm from "../form/ClientForm";
type Prop = {
  clients: Client[];
  handleSelectedClient: (id:number)=>void;
  handleCancelClient:()=>void;
  selectedClient?:Client | undefined;
  openForm:(id:number)=>void;
  closeForm:()=>void;
  editMode:boolean;
};
export default function ClientDashboard({ clients,handleSelectedClient,handleCancelClient,
  selectedClient,openForm,closeForm,editMode }: Prop) {
  return (
    <>
      <Grid2 container spacing={3}>
        <Grid2 size={7}>
         
           <ClientList clients={clients}
             selectClient={handleSelectedClient}
           />
         
        </Grid2>
        <Grid2 size={5}>
          {selectedClient && !editMode && 
          <ClientDetails selectedClient={selectedClient}
          cancelSelectClient= {handleCancelClient}
          openForm={openForm}
          />}
          {editMode && 
          <ClientForm closeForm={closeForm} 
          client={selectedClient}
          />}
        </Grid2>
      </Grid2>
    </>
  );
}
