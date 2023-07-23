create or replace function insert_into_pt_device_info(
    a_device_uid text,
    a_latitude double precision, 
    a_longitude double precision)
returns void as 
$$
declare
   l_pt_device_id integer;
begin
    select d.pt_device_id
    into l_pt_device_id
    from public.pt_device d
    where d.device_uid = a_device_uid; 

    if l_pt_device_id = null then 
        return; 
    end if; 

    insert into public.pt_device_info (pt_device_id, latitude, longitude)
    values (l_pt_device_id, a_latitude, a_longitude); 
end
$$ language plpgsql;
