using AutoMapper;
using EventHandler.Data;
using EventHandler.Data.Models;
using EventHandlerAPI.Interfaces;
using EventHandlerAPI.Views;
using System.Collections.Generic;

namespace EventHandlerAPI.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _MemberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository MemberRepository, IMapper mapper)
        {
            _MemberRepository = MemberRepository;
            _mapper = mapper;
        }

        public async Task<List<MemberView>> GetMembers()
        {
            List<Member> Member = await _MemberRepository.GetMembers();
            return _mapper.Map<List<MemberView>>(Member);
        }

        public async Task<MemberView> GetMember(Guid Id)
        {
            Member Member = await _MemberRepository.GetMember(Id);
            if (Member == null)
            {
                return null;
            }
            return _mapper.Map<MemberView>(Member);
        }

        public async Task<MemberView> Login(string username, string password)
        {
            Member Member = await _MemberRepository.GetMemberByUserName(username);
            if (Member == null)
            {
                throw new Exception("Member does not Exist!!");
            }
            if (Member.Password == password)
            {
                return _mapper.Map<MemberView>(Member);
            }
            else
            {
                throw new Exception("Wrong password");
            }
           
        }

        public async Task<MemberView> AddMember(MemberCreationView MemberCreationView)
        {
            Member MemberModel = _mapper.Map<Member>(MemberCreationView);
            Task<Member> oldMember = _MemberRepository.GetMemberByUserName(MemberCreationView.UserName);
            if (oldMember.Result != null)
            {
                throw new Exception("Member Already Exists");
            }
            MemberModel.MemberId = new Guid();
            Member Member = await _MemberRepository.AddMember(MemberModel);
            await _MemberRepository.SaveAsync();
            return _mapper.Map<MemberView>(Member);
        }

        public async Task DeleteMember(Guid Id)
        {
            _MemberRepository.DeleteMember(Id);
            await _MemberRepository.SaveAsync();
        }

        public async Task UpdateMember(MemberCreationView MemberView)
        {
            Member Member = await _MemberRepository.GetMemberByUserName(MemberView.UserName);
            _mapper.Map(MemberView, Member);
            await _MemberRepository.SaveAsync();
        }
    }
}