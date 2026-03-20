import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";

export function useStates() {
  const queryClient = useQueryClient();

  const list = useQuery({
    queryKey: ["admin", "states"],
    queryFn: async () => {
      const res = await agent.get<StateMst[]>("/Admin/States");
      return res.data;
    },
  });

  const create = useMutation({
    mutationFn: async (dto: Omit<StateMst, "stateId">) => {
      const res = await agent.post<number>("/Admin/States", { ...dto, stateId: 0 });
      return res.data;
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "states"] }),
  });

  const update = useMutation({
    mutationFn: async (dto: StateMst) => {
      await agent.put("/Admin/States", dto);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "states"] }),
  });

  const remove = useMutation({
    mutationFn: async (id: number) => {
      await agent.delete(`/Admin/States/${id}`);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "states"] }),
  });

  return { list, create, update, remove };
}

export function useDistricts() {
  const queryClient = useQueryClient();

  const list = useQuery({
    queryKey: ["admin", "districts"],
    queryFn: async () => {
      const res = await agent.get<DistrictMst[]>("/Admin/Districts");
      return res.data;
    },
  });

  const create = useMutation({
    mutationFn: async (dto: Omit<DistrictMst, "districtId">) => {
      const res = await agent.post<number>("/Admin/Districts", { ...dto, districtId: 0 });
      return res.data;
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "districts"] }),
  });

  const update = useMutation({
    mutationFn: async (dto: DistrictMst) => {
      await agent.put("/Admin/Districts", dto);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "districts"] }),
  });

  const remove = useMutation({
    mutationFn: async (id: number) => {
      await agent.delete(`/Admin/Districts/${id}`);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "districts"] }),
  });

  return { list, create, update, remove };
}

export function useCities() {
  const queryClient = useQueryClient();

  const list = useQuery({
    queryKey: ["admin", "cities"],
    queryFn: async () => {
      const res = await agent.get<CityMst[]>("/Admin/Cities");
      return res.data;
    },
  });

  const create = useMutation({
    mutationFn: async (dto: Omit<CityMst, "cityId">) => {
      const res = await agent.post<number>("/Admin/Cities", { ...dto, cityId: 0 });
      return res.data;
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "cities"] }),
  });

  const update = useMutation({
    mutationFn: async (dto: CityMst) => {
      await agent.put("/Admin/Cities", dto);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "cities"] }),
  });

  const remove = useMutation({
    mutationFn: async (id: number) => {
      await agent.delete(`/Admin/Cities/${id}`);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "cities"] }),
  });

  return { list, create, update, remove };
}

export function useCaseTypes() {
  const queryClient = useQueryClient();

  const list = useQuery({
    queryKey: ["admin", "caseTypes"],
    queryFn: async () => {
      const res = await agent.get<CaseTypeMst[]>("/Admin/CaseTypes");
      return res.data;
    },
  });

  const create = useMutation({
    mutationFn: async (dto: Omit<CaseTypeMst, "caseTypeId">) => {
      const res = await agent.post<number>("/Admin/CaseTypes", { ...dto, caseTypeId: 0 });
      return res.data;
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "caseTypes"] }),
  });

  const update = useMutation({
    mutationFn: async (dto: CaseTypeMst) => {
      await agent.put("/Admin/CaseTypes", dto);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "caseTypes"] }),
  });

  const remove = useMutation({
    mutationFn: async (id: number) => {
      await agent.delete(`/Admin/CaseTypes/${id}`);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "caseTypes"] }),
  });

  return { list, create, update, remove };
}

export function useCourtTypes() {
  const queryClient = useQueryClient();

  const list = useQuery({
    queryKey: ["admin", "courtTypes"],
    queryFn: async () => {
      const res = await agent.get<CourtTypeMst[]>("/Admin/CourtTypes");
      return res.data;
    },
  });

  const create = useMutation({
    mutationFn: async (dto: Omit<CourtTypeMst, "courtTypeId">) => {
      const res = await agent.post<number>("/Admin/CourtTypes", { ...dto, courtTypeId: 0 });
      return res.data;
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "courtTypes"] }),
  });

  const update = useMutation({
    mutationFn: async (dto: CourtTypeMst) => {
      await agent.put("/Admin/CourtTypes", dto);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "courtTypes"] }),
  });

  const remove = useMutation({
    mutationFn: async (id: number) => {
      await agent.delete(`/Admin/CourtTypes/${id}`);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "courtTypes"] }),
  });

  return { list, create, update, remove };
}

export function useCourts() {
  const queryClient = useQueryClient();

  const list = useQuery({
    queryKey: ["admin", "courts"],
    queryFn: async () => {
      const res = await agent.get<CourtMst[]>("/Admin/Courts");
      return res.data;
    },
  });

  const create = useMutation({
    mutationFn: async (dto: Omit<CourtMst, "courtId">) => {
      const res = await agent.post<number>("/Admin/Courts", { ...dto, courtId: 0 });
      return res.data;
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "courts"] }),
  });

  const update = useMutation({
    mutationFn: async (dto: CourtMst) => {
      await agent.put("/Admin/Courts", dto);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "courts"] }),
  });

  const remove = useMutation({
    mutationFn: async (id: number) => {
      await agent.delete(`/Admin/Courts/${id}`);
    },
    onSuccess: async () => queryClient.invalidateQueries({ queryKey: ["admin", "courts"] }),
  });

  return { list, create, update, remove };
}

