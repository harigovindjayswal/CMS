import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { toast } from "react-toastify";

type CreateCasePayload = {
  lawyerRequestId: number;
  caseTypeId: number;
  courtId?: number | null;
  courtName?: string;
  title: string;
  description?: string;
  caseNumber?: string;
  purpose?: string;
  filingDate?: string | null;
};

type UpdateStatusPayload = {
  caseId: number;
  status: string;
  stage: string;
};

type AddNotePayload = {
  caseId: number;
  content: string;
  isPrivate: boolean;
};

export const useCases = (mode: "client" | "lawyer") => {
  const queryClient = useQueryClient();

  const listQuery = useQuery({
    queryKey: ["cases", mode],
    queryFn: async () => {
      const url = mode === "client" ? "/Clients/Cases" : "/Lawyer/Cases";
      const response = await agent.get<CaseItem[]>(url);
      return response.data;
    },
    retry: false,
  });

  const detailsQuery = (id?: number) =>
    useQuery({
      queryKey: ["cases", mode, id],
      queryFn: async () => {
        const url = mode === "client" ? `/Clients/Cases/${id}` : `/Lawyer/Cases/${id}`;
        const response = await agent.get<CaseDetails>(url);
        return response.data;
      },
      enabled: !!id,
      retry: false,
    });

  const createCase = useMutation({
    mutationFn: async (payload: CreateCasePayload) => {
      const response = await agent.post<number>("/Lawyer/Cases", payload);
      return response.data;
    },
    onSuccess: async () => {
      toast.success("Case created");
      await queryClient.invalidateQueries({ queryKey: ["cases", "lawyer"] });
    },
  });

  const updateStatus = useMutation({
    mutationFn: async (payload: UpdateStatusPayload) => {
      await agent.put("/Lawyer/Cases/status", payload);
    },
    onSuccess: async () => {
      toast.success("Case updated");
      await queryClient.invalidateQueries({ queryKey: ["cases", "lawyer"] });
    },
  });

  const addNote = useMutation({
    mutationFn: async (payload: AddNotePayload) => {
      const url = mode === "client" ? "/Clients/Cases/notes" : "/Lawyer/Cases/notes";
      await agent.post(url, payload);
    },
    onSuccess: async (_data, variables) => {
      toast.success("Note added");
      await queryClient.invalidateQueries({ queryKey: ["cases", mode, variables.caseId] });
    },
  });

  const uploadDocument = useMutation({
    mutationFn: async (input: { caseId: number; file: File; title?: string; category?: string }) => {
      const formData = new FormData();
      formData.append("file", input.file);
      if (input.title) formData.append("title", input.title);
      if (input.category) formData.append("category", input.category);
      await agent.post(`/Lawyer/Cases/${input.caseId}/documents`, formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
    },
    onSuccess: async (_data, variables) => {
      toast.success("Document uploaded");
      await queryClient.invalidateQueries({ queryKey: ["cases", "lawyer", variables.caseId] });
    },
  });

  return {
    listQuery,
    detailsQuery,
    createCase,
    updateStatus,
    addNote,
    uploadDocument,
  };
};

