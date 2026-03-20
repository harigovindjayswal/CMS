import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { toast } from "react-toastify";

type RespondPayload = {
  lawyerRequestId: number;
  status: number; // 3 Accepted, 4 Rejected
  lawyerRemark: string;
};

type CreatePayload = {
  lawyerId: number;
  caseTypeId: number;
  stateId: number;
  districtId: number;
  cityId: number;
  caseDescription: string;
};

export const useLawyerRequests = (mode: "client" | "lawyer") => {
  const queryClient = useQueryClient();

  const listQuery = useQuery({
    queryKey: ["lawyerRequests", mode],
    queryFn: async () => {
      const url =
        mode === "client"
          ? "/Clients/LawyerRequests"
          : "/Lawyer/LawyerRequests";
      const response = await agent.get<LawyerRequest[]>(url);
      return response.data;
    },
    retry: false,
  });

  const respondMutation = useMutation({
    mutationFn: async (payload: RespondPayload) => {
      await agent.put("/Lawyer/LawyerRequests/respond", payload);
    },
    onSuccess: async () => {
      toast.success("Response saved");
      await queryClient.invalidateQueries({ queryKey: ["lawyerRequests", "lawyer"] });
    },
  });

  const createMutation = useMutation({
    mutationFn: async (payload: CreatePayload) => {
      console.log(payload);
      const response = await agent.post<number>("/Clients/LawyerRequests", payload);
      return response.data;
    },
    onSuccess: async () => {
      toast.success("Request submitted");
      await queryClient.invalidateQueries({ queryKey: ["lawyerRequests", "client"] });
    },
  });

  return {
    listQuery,
    respondMutation,
    createMutation,
  };
};
