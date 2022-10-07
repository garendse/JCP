import { useAuth } from "../auth/AuthProvider";

export function UserAdmin() {
  const auth = useAuth();
  return (
    <>
      <span>{auth.user.id}</span>
      <span>{auth.user.username}</span>
      <span>{auth.user.name}</span>
      <span>{auth.user.password_date}</span>
    </>
  );
}
