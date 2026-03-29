import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";

export function useManagedClients() {
  const qc = useQueryClient();

  const list = useQuery({
    queryKey: ["lawyerAdmin", "clients"],
    queryFn: async () => {
      const res = await agent.get<ManagedClient[]>("/LawyerAdmin/Users/clients");
      return res.data;
    },
  });

  const create = useMutation({
    mutationFn: async (dto: { email: string; displayName: string; password: string; firstName?: string; middleName?: string; lastName?: string; mobileNo?: string }) => {
      const res = await agent.post<number>("/LawyerAdmin/Users/clients", dto);
      return res.data;
    },
    onSuccess: async () => qc.invalidateQueries({ queryKey: ["lawyerAdmin", "clients"] }),
  });

  return { list, create };
}

export function useManagedLawyers() {
  const qc = useQueryClient();

  const list = useQuery({
    queryKey: ["lawyerAdmin", "lawyers"],
    queryFn: async () => {
      const res = await agent.get<ManagedLawyer[]>("/LawyerAdmin/Users/lawyers");
      return res.data;
    },
  });

  const create = useMutation({
    mutationFn: async (dto: { email: string; displayName: string; password: string; firstName?: string; middleName?: string; lastName?: string; mobileNo?: string; stateId?: number | null; cityId?: number | null; barLicenseNumber?: string | null }) => {
      const res = await agent.post<number>("/LawyerAdmin/Users/lawyers", dto);
      return res.data;
    },
    onSuccess: async () => qc.invalidateQueries({ queryKey: ["lawyerAdmin", "lawyers"] }),
  });

  return { list, create };
}

export function useManagedStaff() {
  const qc = useQueryClient();

  const list = useQuery({
    queryKey: ["lawyerAdmin", "staff"],
    queryFn: async () => {
      const res = await agent.get<ManagedStaff[]>("/LawyerAdmin/Users/staff");
      return res.data;
    },
  });

  const create = useMutation({
    mutationFn: async (dto: { email: string; displayName: string; password: string; lawyerId: number; firstName?: string; middleName?: string; lastName?: string; mobileNo?: string }) => {
      const res = await agent.post<number>("/LawyerAdmin/Users/staff", dto);
      return res.data;
    },
    onSuccess: async () => qc.invalidateQueries({ queryKey: ["lawyerAdmin", "staff"] }),
  });

  return { list, create };
}

export function useManagedCases() {
  const qc = useQueryClient();

  const list = useQuery({
    queryKey: ["lawyerAdmin", "cases"],
    queryFn: async () => {
      const res = await agent.get<ManagedCase[]>("/LawyerAdmin/Cases");
      return res.data;
    },
  });

  const detailsQuery = (id?: number) =>
    useQuery({
      queryKey: ["lawyerAdmin", "cases", id],
      queryFn: async () => {
        const res = await agent.get<CaseDetails>(`/LawyerAdmin/Cases/${id}`);
        return res.data;
      },
      enabled: !!id,
      retry: false,
    });

  const createManual = useMutation({
    mutationFn: async (dto: { clientId: number; lawyerId: number; caseTypeId: number; courtId?: number | null; courtName?: string | null; title: string; description?: string | null; caseNumber?: string | null; purpose?: string | null; filingDate?: string | null }) => {
      const res = await agent.post<number>("/LawyerAdmin/Cases/manual", dto);
      return res.data;
    },
    onSuccess: async () => {
      await qc.invalidateQueries({ queryKey: ["lawyerAdmin", "cases"] });
    },
  });

  return { list, detailsQuery, createManual };
}
